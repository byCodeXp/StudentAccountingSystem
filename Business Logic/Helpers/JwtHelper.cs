using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Data_Transfer_Objects.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business_Logic.Helpers
{
    public class JwtModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string Role { get; set; }
        public DateTime Expires { get; set; }
    }
    
    public interface IJwtHelper
    {
        public string GenerateToken(UserDTO user, TimeSpan duration);
        public JwtModel DecodeToken(string token);
        public JwtSecurityToken Verify(string token);
    }

    public class JwtHelper : IJwtHelper
    {
        private readonly SymmetricSecurityKey symmetricSecurityKey;
        
        public JwtHelper(IConfiguration configuration)
        {
            var secret = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            symmetricSecurityKey = new SymmetricSecurityKey(secret);
        }

        public string GenerateToken(UserDTO user, TimeSpan duration)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim("firstName", user.FirstName),
                    new Claim("lastName", user.LastName),
                    new Claim("email", user.Email),
                    new Claim("birthDay", user.BirthDay.ToString(CultureInfo.InvariantCulture)),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.Add(duration),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public JwtSecurityToken Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters()
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;
        }

        public JwtModel DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            return new JwtModel
            {
                Id = GetClaim("id", jwtSecurityToken).Value,
                FirstName = GetClaim("firstName", jwtSecurityToken).Value,
                LastName = GetClaim("lastName", jwtSecurityToken).Value,
                Email = GetClaim("email", jwtSecurityToken).Value,
                BirthDay = DateTime.Parse(GetClaim("birthDay", jwtSecurityToken).Value),
                Role = GetClaim("role", jwtSecurityToken).Value,
                Expires = jwtSecurityToken.ValidTo
            };
        }

        private Claim GetClaim(string claim, JwtSecurityToken jwtSecurityToken)
        {
            return jwtSecurityToken.Claims.FirstOrDefault(m => m.Type == claim);
        }
    }
}