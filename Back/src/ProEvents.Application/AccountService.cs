using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        public Task<bool> UserExists(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserUpdateDto> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
