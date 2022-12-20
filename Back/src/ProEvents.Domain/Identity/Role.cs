using Microsoft.AspNetCore.Identity;

namespace ProEvents.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
