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
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]User userInfo)
        {
            var user = _service.Authenticate(userInfo.UserName, userInfo.Password);
            if (!user.Success)
            {
                return BadRequest(user.Message);
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User userInfo)
        {
            var result = await _service.Register(userInfo);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}