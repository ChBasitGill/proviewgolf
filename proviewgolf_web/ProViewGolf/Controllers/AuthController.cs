using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProViewGolf.Core.Dbo.Entities;
using ProViewGolf.Core.Dbo.Models;
using ProViewGolf.Core.Services;
using ProViewGolf.DataLayer.Models;

namespace ProViewGolf.Controllers
{
    [ApiController, Route("api/[controller]/[Action]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult SignUp(User model)
        {
            var r = _userService.SignUp(model, out var msg);

            var response = new Response
            {
                Msg = msg, 
                Status = r ? ResponseStatus.Success : ResponseStatus.Error
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Authenticate(PasswordModel model)
        {
            var responseHeaders = HttpContext.Response.Headers;
            var response = new Response();

            try
            {
                var user = _userService.Authenticate(model, out var msg);
                response.Msg = msg;

                if (user == null)
                {
                    response.Status = ResponseStatus.Error;
                    return Ok(response);
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow + ""),
                    new Claim("UserId", user.UserId.ToString()),
                    //new Claim("UserName", response.Data.FullName),
                    new Claim("Email", user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var securityToken = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: signin
                );

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                user.Token = token;

                responseHeaders.Add("Authorization", token);
                responseHeaders.Add("Access-Control-Expose-Headers", "Authorization");

                response.Data = user;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Msg = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpGet("{token:Guid}")]
        public IActionResult ConfirmEmail([FromRoute] string token)
        {
            var r = _userService.ConfirmEmail(token, out var msg);
            return Ok(msg);

        }

        [HttpPost]
        public IActionResult RecoverPassword(PasswordModel model)
        {
            var r = _userService.RecoverPassword(model.Email, out var msg);
            var response = new Response {Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error};

            return Ok(response);
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordModel model)
        {
            var r = _userService.ChangePassword(model, out var msg);
            var response = new Response {Msg = msg, Status = r ? ResponseStatus.Success : ResponseStatus.Error};

            return Ok(response);
        }

        [HttpGet, Route("{code}/{accept}")]
        public IActionResult Invitation([FromRoute] string code, [FromRoute] int accept)
        {
            _userService.Invitation(code, accept, out var msg);
            return Ok(msg);
        }
    }
}