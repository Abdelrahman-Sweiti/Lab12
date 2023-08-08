using Lab12.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lab12.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUser registerUser,ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username,string password);


    }
}
