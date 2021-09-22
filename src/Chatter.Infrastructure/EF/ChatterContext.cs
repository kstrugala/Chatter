using Chatter.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chatter.Infrastructure.EF
{
    public class ChatterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public DbSet<Post> Posts { get; set; }

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
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);
            refreshTokenBuilder.ToTable("RefreshToken", schema: "User");

            var postBuilder = modelBuilder.Entity<Post>();
            postBuilder.HasKey(p => p.Id);
            postBuilder.Property(u => u.UniqueId)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .IsRequired();
            postBuilder
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId);
             
            postBuilder.ToTable("Post", schema: "User");


        }
    }
}
