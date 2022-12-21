using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ProEvents.Application.Dto
{
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
