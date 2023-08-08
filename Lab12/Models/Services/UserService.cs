using Lab12.Models.DTO;
using Lab12.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lab12.Models.Services
{
    public class UserService : IUser
    {
        private UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> manager )
        {
            userManager = manager;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool ValidPassowrd = await userManager.CheckPasswordAsync(user, password);

            if (ValidPassowrd)
            {
                return new UserDTO { Id = user.Id, Username = user.UserName };
            }
            return null;
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
                return new UserDTO()
                {
                    Id = user.Id,
                    Username = user.UserName

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
