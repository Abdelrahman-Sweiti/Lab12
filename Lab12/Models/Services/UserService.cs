using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab12.Models.Services
{
    public class UserService : IUser
    {
        private UserManager<ApplicationUser> userManager;
        private JwtTokenService tokenService;

        public UserService(UserManager<ApplicationUser> manager , JwtTokenService tokenService)
        {
            userManager = manager;
            this.tokenService = tokenService;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool ValidPassowrd = await userManager.CheckPasswordAsync(user, password);

            if (ValidPassowrd)
            {
                return new UserDTO { Id = user.Id, Username = user.UserName, Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)) };
            }
            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
            };
        }

        public async Task<UserDTO> Register(RegisterUser registerUser, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                PhoneNumber = registerUser.Phone

            };

            var result = await userManager.CreateAsync(user,registerUser.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, registerUser.Roles);

                return new UserDTO()
                {
                    Id = user.Id,
                    Username = user.UserName,
                     Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))

                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUser.Password):
                               error.Code.Contains("Email") ? nameof(registerUser.Email) :
                               error.Code.Contains("Username") ? nameof(registerUser.Username) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;

        }
    }
}
