using Microsoft.EntityFrameworkCore;
using scrum_board_tool.Shared;

namespace scrum_board_tool.Server.Model
{
    public class ScrumBoardDbContext : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<Sprint> Sprint { get; set; }
        public DbSet<BacklogItem> BacklogItem { get; set; }
        public DbSet<Shared.Task> Task { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");
        }

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
                entity.HasKey(e =>e.Id);
                entity.Property(e =>e.Name).IsRequired();
                entity.HasOne(e => e.Sprint).WithMany(e => e.BacklogItems);
            });

            modelBuilder.Entity<Shared.Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.BacklogItem).WithMany(e => e.Tasks);
                entity.HasOne(e => e.User).WithMany(e => e.Tasks);
            });
        }
    }
}
