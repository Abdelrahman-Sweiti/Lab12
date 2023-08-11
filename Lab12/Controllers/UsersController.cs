using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Lab12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUser userService;

        public UsersController(IUser service)
        {
            userService = service;
        }

        [Authorize(Roles = "District Manager")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUser data)
        { 
        var user = await userService.Register(data,this.ModelState);

            if (ModelState.IsValid)
            {
                return user;

            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO log) {

            var user = await userService.Authenticate(log.Username,log.Password);

            if (user == null)
            {
                return Unauthorized();

            }
            return user;
        }
        [Authorize(Policy = "create")]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            return await userService.GetUser(this.User); ;
        }

    }
}
