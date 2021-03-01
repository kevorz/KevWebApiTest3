using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TodoSqlite.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<TodoItem>().ToTable("Items", "test");
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id); //.IsUnique()
                entity.HasIndex(e => e.Name);
                entity.Property(e => e.IsComplete);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}