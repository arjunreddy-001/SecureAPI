using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecureAPI.Models;
using SecureAPI.Models.BindingModels;
using SecureAPI.Models.DTOs;
using SecureAPI.Services;

namespace SecureAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authSvc;

        public AuthController(IAuthService authService)
        {
            _authSvc = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginBindingModel model)
        {
            var authUser = _authSvc.Authenticate(model.Username, model.Password);

            if(authUser == null)
            {
                return BadRequest(new {
                    message = "Authentication failed !!"
                });
            }

            return Ok(new AuthUserDTO(authUser.FirstName, authUser.LastName, authUser.UserName, authUser.Token));
        }
    }
}