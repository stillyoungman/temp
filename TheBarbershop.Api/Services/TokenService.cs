using AutoMapper.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheBarbershop.Api.Models;

namespace TheBarbershop.Api.Services
{
    public class TokenService
    {
        private readonly string secret;

        public TokenService(IOptions<ApplicationConfiguration> options)
        {
            secret = options.Value.TokenSecret;
        }

        public string IssueToken(IUserInfo userInfo)
        {
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Subject),
                new Claim("role",userInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: "DUMMY_ISSUER",
                audience: "DUMMY_AUDIENCE",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
