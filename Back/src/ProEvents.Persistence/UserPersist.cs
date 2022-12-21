using ProEvents.Domain.Identity;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Contracts;

namespace ProEvents.Persistence
{
    public class UserPersist : GeneralPersist, IUserPersist
    {
        private readonly ProEventsContext _context;

        public UserPersist(ProEventsContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
