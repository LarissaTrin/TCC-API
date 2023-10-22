using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs;
using Project.Application.Services;
using Project.Domain.Identity;
using Project.Persistence.IPersistence;

namespace Project.Application
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly IMapper _mapper;
        public readonly IUserPersistence _userPersistence;
        public AccountService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            IMapper mapper,
                            IUserPersistence userPersistence)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersistence = userPersistence;
            
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDTO userUpdateDTO, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDTO.UserName.ToLower());
                return await _signInManager.CheckPasswordSignInAsync(user, password,false);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error when trying to verify the password. Error: {ex.Message}");
            }
        }

        public async Task<UserUpdateDTO> CreateAccountAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDTO>(user);
                    return userToReturn;
                }
                    return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error when trying to create a user. Error: {ex.Message}");
            }
        }

        public async Task<UserUpdateDTO> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersistence.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                var userUpdateDTO = _mapper.Map<UserUpdateDTO>(user);
                return userUpdateDTO;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error when trying to find a user by UserName. Error: {ex.Message}");
            }
        }

        public async Task<UserUpdateDTO> UpdateAccount(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _userPersistence.GetUserByUserNameAsync(userUpdateDTO.UserName);
                if (user == null) return null;

                userUpdateDTO.Id = user.Id;

                _mapper.Map(userUpdateDTO, user);

                if(userUpdateDTO.Password != null){
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDTO.Password);
                }

                _userPersistence.Update<User>(user);

                if (await _userPersistence.SaveChangesAsync())
                {
                    var userReturn = await _userPersistence.GetUserByUserNameAsync(user.UserName);
                    return _mapper.Map<UserUpdateDTO>(userReturn);
                }
                return null;
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error when trying to update a user. Error: {ex.Message}");
            }
        }

        public async Task<bool> UserExist(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error when verifying if the user exists. Error: {ex.Message}");
            }
        }
    }
}