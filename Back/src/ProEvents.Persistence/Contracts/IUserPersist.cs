using ProEvents.Domain.Identity;

namespace ProEvents.Persistence.Contracts
{
    public interface IUserPersist : IGeneralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
