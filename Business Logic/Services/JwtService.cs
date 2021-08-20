using Data_Transfer_Objects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business_Logic.Services
{
    public class JwtService
    {
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public JwtService(IConfiguration configuration)
        {
            var secret = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            _symmetricSecurityKey = new SymmetricSecurityKey(secret);
        }

        public string WriteToken(UserDTO user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = user.Id, // TODO: send once
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id), // TODO: send once
                    new Claim("email", user.Email),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
            {
                IssuerSigningKey = _symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;
        }
    }
}
