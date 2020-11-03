using System.IO;
using Author.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Author.Persistence.Context
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<Core.Entity.Author> Authors { get; set; }
        
        public AuthorDbContext(DbContextOptions<AuthorDbContext> Options)
            : base(Options)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
    }
}