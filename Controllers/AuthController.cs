using CapteurManagement.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapteurManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModelDTO login)
        {
            // Example of hardcoded credentials validation
            if (login.Username != "admin" || login.Password != "admin123")
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Create token claims
            var claims = new[]
            {
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, login.Username),
            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Generate the token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidIssuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
