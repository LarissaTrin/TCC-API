using Project.Application.DTOs;

namespace Project.Application.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDTO userUpdateDTO);
    }
}