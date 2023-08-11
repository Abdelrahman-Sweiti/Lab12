using Lab12.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Lab12.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUser registerUser,ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username,string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
    }
}
