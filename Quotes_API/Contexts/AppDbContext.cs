using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quotes_API.Models;

namespace Quotes_API.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<LoginUser> Users { get; set; }

        public DbSet<UserAndQuote> UsersAndQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAndQuote>()
                .HasKey(uq => new { uq.QuoteId, uq.UserEmail });
        }

    }
}
