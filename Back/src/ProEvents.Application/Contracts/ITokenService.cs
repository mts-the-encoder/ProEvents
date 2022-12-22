using ProEvents.Application.Dto;

namespace ProEvents.Application.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
