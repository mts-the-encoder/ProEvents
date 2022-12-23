using Microsoft.AspNetCore.Identity;

namespace ProEvents.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
