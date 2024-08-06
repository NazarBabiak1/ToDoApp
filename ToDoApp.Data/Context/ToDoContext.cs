using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoApp.Data.Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(2000);

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .Property(t => t.CreateAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .HasOne(t => t.User) 
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.CreatedById);

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .HasOne(t => t.Board) 
                .WithMany(b => b.Tasks)
                .HasForeignKey(t => t.BoardId);

            modelBuilder.Entity<ToDoApp.Data.Models.Task>()
                .HasOne(t => t.Status) 
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Board>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Board>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("VARCHAR(255)");

            modelBuilder.Entity<Board>()
                .Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            modelBuilder.Entity<Status>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Status>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
