using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;
using ChooseMemeWebServer.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChooseMemeWebServer.Infrastructure.Services
{
    public class JWTTokenService(IOptions<JWTOptions> jwtOptions) : ITokenService
    {
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: new List<Claim>()
                {
                    new Claim("UserId", user.Id)
                },
                expires: DateTime.Now.AddMinutes(jwtOptions.Value.ExpiryInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
