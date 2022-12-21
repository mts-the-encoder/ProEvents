using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEvents.Application.Contracts;
using ProEvents.Application.Dto;
using ProEvents.Domain.Identity;
using ProEvents.Persistence.Contracts;

namespace ProEvents.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager, 
                              SignInManager<User> signInManager, 
                              IMapper mapper,
                              IUserPersist userPersist)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }
        public async Task<bool> UserExists(string userName)
        {
            try
            {
                return await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception($"Error to get user. Error: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var user = await _userPersist.GetUserByUserNameAsync(userName);
                if (user == null) return null;

                var userUpdate = _mapper.Map<UserUpdateDto>(user);
                return userUpdate;
            }
            catch (Exception e)
            {
                throw new Exception($"Error to get username. Error: {e.Message}");
            }   
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users
                    .SingleOrDefaultAsync(x => x.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, true);
            }
            catch (Exception e)
            {
                throw new Exception($"Error to check password. Error: {e.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded) return null;
                var userToReturn = _mapper.Map<UserDto>(user);
                return userToReturn;

            }
            catch (Exception e)
            {
                throw new Exception($"Error to create acc. Error: {e.Message}");
            }   
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                throw new Exception($"Error to update acc. Error: {e.Message}");
            }   
        }
    }
}
