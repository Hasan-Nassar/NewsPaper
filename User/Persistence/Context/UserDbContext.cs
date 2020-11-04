using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace User.Persistence.Context
{
    public class UserDbContext: DbContext
    {
        public DbSet<Core.Entity.User> Users { get; set; }
        
        public UserDbContext(DbContextOptions<UserDbContext> Options)  : base(Options)
        {
        }
    }
    
    public class BloggingContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=User;Trusted_Connection=True;");

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}