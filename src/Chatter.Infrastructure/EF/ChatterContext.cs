using Chatter.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chatter.Infrastructure.EF
{
    public class ChatterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ChatterContext(DbContextOptions<ChatterContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.HasKey(u => u.Id);
            userBuilder.Property(u => u.UniqueId)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .IsRequired();
            userBuilder.ToTable("Profile", schema: "User");

            var refreshTokenBuilder = modelBuilder.Entity<RefreshToken>();
            refreshTokenBuilder.HasKey(r => r.Token);
            refreshTokenBuilder
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);
            refreshTokenBuilder.ToTable("RefreshToken", schema: "User");
        }
    }
}
