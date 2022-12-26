using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUserNameAsync(string? userName)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
