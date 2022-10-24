using Microsoft.EntityFrameworkCore;
using ProEvents.API.Models;

namespace ProEvents.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; }
    }
}
