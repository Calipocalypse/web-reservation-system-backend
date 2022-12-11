using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Wsr.Controllers.Misc;
using Wsr.Data;
using Wsr.Misc;
using Wsr.Models.Authentication;
using Wsr.Models.JsonModels;

namespace Wsr.Controllers.Authentication
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            var message = new Error($"User of name {userLogin.Username} has not been found");
            return NotFound(message);
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            using (var apiContext = new ApiContext())
            {
                var notVerifiedUser = apiContext.Users.FirstOrDefault(x => x.UserName == userLogin.Username);

                bool isPasswordMatches = false;
                if (notVerifiedUser != null)
                {
                    isPasswordMatches = Hasher.VerifyHash(notVerifiedUser.HashedPassword, notVerifiedUser.Salt, userLogin.Password);
                }

                if (isPasswordMatches)
                {
                    var authenticatedUser = notVerifiedUser;
                    return (authenticatedUser);
                }
            }

            return null;
        }
    }
}
