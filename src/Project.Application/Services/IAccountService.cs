using Microsoft.AspNetCore.Identity;
using Project.Application.DTOs;

namespace Project.Application.Services
{
    public interface IAccountService
    {
        Task<bool> UserExist(string userName);
        Task<UserUpdateDTO> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDTO userUpdateDTO, string password);
        Task<UserUpdateDTO> CreateAccountAsync(UserDTO userDTO);
        Task<UserUpdateDTO> UpdateAccount(UserUpdateDTO userUpdateDTO);
    }
}