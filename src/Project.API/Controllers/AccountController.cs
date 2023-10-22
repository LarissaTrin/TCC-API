using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.API.Extensions;
using Project.Application.DTOs;
using Project.Application.Services;

namespace Project.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,
                                ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve a user. Erro: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(UserDTO userDTO)
        {
            try
            {
                if (await _accountService.UserExist(userDTO.UserName)) return BadRequest("User already exists.");

                var user = await _accountService.CreateAccountAsync(userDTO);
                if (user != null) return  Ok(
                    new {
                        id = user.Id,
                        userName = user.UserName,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        token = _tokenService.CreateToken(user).Result
                    }
                );

                return BadRequest("Error when trying to register a user.");
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve a user. Erro: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginDTO userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.UserName);
                if (user == null) return Unauthorized("Invalid User or Password.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized("Invalid User or Password.");

                return Ok(new {
                    id = user.Id,
                    userName = user.UserName,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to retrieve a user. Erro: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            try
            {
                if (userUpdateDTO.UserName != User.GetUserName()) return Unauthorized("Invalid User");

                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Invalid User.");

                var userReturn = await _accountService.UpdateAccount(userUpdateDTO);
                if (userReturn == null) return NoContent();

                return Ok(new {
                    id = user.Id,
                    userName = userReturn.UserName,
                    firstName = userReturn.FirstName,
                    lastName = userReturn.LastName,
                    token = _tokenService.CreateToken(userReturn).Result
                });
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error when trying to update a user. Erro: {ex.Message}");
            }
        }
    }
}