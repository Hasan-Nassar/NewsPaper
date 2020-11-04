using System.IO;
using Author.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Author.Persistence.Context
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<Core.Entity.Author> Authors { get; set; }
        
        public AuthorDbContext(DbContextOptions<AuthorDbContext> Options) : base(Options)
        {
        }
    }
    
    
    public class BloggingContextFactory : IDesignTimeDbContextFactory<AuthorDbContext>
    {
        public AuthorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=NewsPaper;Trusted_Connection=True;");

            return new AuthorDbContext(optionsBuilder.Options);
        }
    }
}