using Chatter.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure.EF
{
    public class ChatterContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ChatterContext(DbContextOptions<ChatterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
