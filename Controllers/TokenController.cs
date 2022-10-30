using FlightDocsSystem.Models;
using FlightDocsSystem.Model.ViewModel;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using FlightDocsSystem.Interface;
using FlightDocsSystem.Model;
using Microsoft.AspNetCore.Authorization;

namespace FlightDocsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TokenController : ControllerBase
    {
        public IAuthentication _authentication;
        public IConfiguration _configuration;
        public TokenController(IAuthentication authentication, IConfiguration configuration)
        {
            _authentication = authentication;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<IActionResult> Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _authentication.Login(viewLogin);

                if (user != null)
                {
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                            new Claim("Id", user.UserId.ToString()),
                            new Claim("UserName", user.UserName),
                            new Claim("Email", user.UserEmail),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                        claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    ViewToken viewToken = new ViewToken()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        User = user
                    };
                    return Ok(viewToken);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{email}")]
        //[Authorize(Roles = "Admin")]
        [ActionName("ChangePassAdmin")]
        public async Task<ActionResult<int>> ChangePass(string email, UserModel userModel)
        {
            if (email != userModel.UserEmail)
            {
                return BadRequest();
            }
            try
            {
                await _authentication.ChangePassword(email, userModel);
                userModel.UserEmail = email;
            }
            catch (Exception ex)
            {
                return BadRequest(-1);
            }

            return Ok(1);
        }

    }
}