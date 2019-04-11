using BJ.BusinessLogic.Options;
using BJ.BusinessLogic.Providers.Interfaces;
using BJ.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BJ.BusinessLogic.Providers
{
    public class JwtTokenProvider:IJwtTokenProvider
    {   private readonly JwtTokenOptions _options;

        public JwtTokenProvider(IOptions<JwtTokenOptions> option)
        {
            _options = option.Value;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_options.ExpireDays));

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
    }
}
    

