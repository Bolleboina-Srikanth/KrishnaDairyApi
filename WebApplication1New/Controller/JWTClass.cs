using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaDairyDotNetApi.Controller
{
    public class JWTClass
    {
        private readonly IConfiguration _configuration;

        public JWTClass( IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string GenerateJwtToken(string Email, long UserId)
        {
            var claims = new List<Claim>
                {
                new Claim("UserId", UserId.ToString()),
                new Claim("Email" , Email)
                };
            // You can add more claims as needed, such as roles or custom claims.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(12), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
