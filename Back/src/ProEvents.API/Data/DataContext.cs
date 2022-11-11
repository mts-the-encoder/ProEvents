using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

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
