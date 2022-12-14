using Microsoft.EntityFrameworkCore;
using WebApplication6.Entity;

namespace WebApplication6.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}