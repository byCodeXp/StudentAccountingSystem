using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Data_Transfer_Objects.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business_Logic.Helpers
{
    public interface IJwtHelper
    {
        public string GenerateToken(UserDTO user, TimeSpan duration);
        public string DecodeUserId(string token);
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
                    new Claim("role", user.Role),
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

        public string DecodeUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.FirstOrDefault(m => m.Type == "id")?.Value;
        }
    }
}