using ExpenseConsult.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseConsult.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options){   }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasIndex(u => u.Email)
                .IsUnique(); 

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Expense>().ToTable("Expenses");

            base.OnModelCreating(modelBuilder);
        }
    }
}
