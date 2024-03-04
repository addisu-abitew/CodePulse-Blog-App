// using Microsoft.EntityFrameworkCore;
using CodePulse.Backend.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Catogories { get; set; }
    }
}