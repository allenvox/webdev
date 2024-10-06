using Microsoft.EntityFrameworkCore;
using lab_3.Models;

namespace lab_3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<lab_3.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношений между Employee и Task
            modelBuilder.Entity<lab_3.Models.Task>()
                .HasOne(t => t.Executor)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.ExecutorId)
                .OnDelete(DeleteBehavior.Restrict); // Укажите поведение при удалении, если это необходимо

            modelBuilder.Entity<lab_3.Models.Task>()
                .HasOne(t => t.Author)
                .WithMany() // У автора нет коллекции задач, поэтому указываем WithMany()
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

