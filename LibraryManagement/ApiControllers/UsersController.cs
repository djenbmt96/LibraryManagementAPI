using System;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Config;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _baseService;

        public UsersController(IUserService baseService)
        {
            _baseService = baseService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]User userInfo)
        {
            var user = _baseService.Authenticate(userInfo.UserName, userInfo.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect", status = 400 });
            }
            return Ok(new { message = "Login success", status = 200, result = user });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User userInfo)
        {
            var result = await _baseService.Register(userInfo);
            if (!result.Success)
            {
                return BadRequest(new { message = "Your username already existed", status = 400 });
            }
            return Ok(new { message = "Account is created", status = 200 });
        }
    }
}