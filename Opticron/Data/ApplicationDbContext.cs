using Microsoft.EntityFrameworkCore;
using Opticron.Models;

namespace Opticron.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
