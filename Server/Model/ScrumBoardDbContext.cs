﻿using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Model
{
    public class ScrumBoardDbContext : DbContext
    {
        public ScrumBoardDbContext()
        {
        }

        public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<Sprint> Sprint { get; set; } = null!;
        public DbSet<BacklogItem> BacklogItem { get; set; } = null!;
        public DbSet<WorkTask> Task { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("DBCONNECTIONSTRING"), new MySqlServerVersion(new Version(8, 1, 0)));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
                entity.HasOne(d => d.Project)
                    .WithMany(s => s.Sprints);
            });

            modelBuilder.Entity<BacklogItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.State).HasConversion<string>();
                entity.HasOne(e => e.Sprint).WithMany(e => e.BacklogItems).IsRequired(false);
            });

            modelBuilder.Entity<WorkTask>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.BacklogItem).WithMany(e => e.Tasks);
                entity.Property(e => e.State).HasConversion<string>();
                entity.HasOne(e => e.User).WithMany(e => e.Tasks).IsRequired(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Role).HasConversion<string>();
                entity.HasOne(d => d.Project)
                    .WithMany(s => s.Users);
            });
        }
    }
}
