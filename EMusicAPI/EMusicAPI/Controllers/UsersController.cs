using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EMusicAPI.Entity;
using EMusicAPI.Models.DataFilter;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using EMusicAPI.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(IUserService userService,  UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _userService.LoginAsync(model);
            if (response == null)
                return Ok(new Response<AuthenticateResponse>(response).Succeeded = false);
            return Ok(new Response<AuthenticateResponse>(response));
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
           

            var response = await _userService.RegisterAsync(model);
            return Ok(new Response<ApplicationUser>(response));
        }

      

        
    }
}
