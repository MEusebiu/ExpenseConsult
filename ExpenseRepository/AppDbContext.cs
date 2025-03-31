using ExpenseDataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){   }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .ToTable("Categories")
            .HasIndex(u => u.Name)
            .IsUnique();  

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Expenses)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId);


        modelBuilder.Entity<Expense>()
            .ToTable("Expenses")
            .HasOne(e => e.Category)  
            .WithMany(c => c.Expenses) 
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); 

        base.OnModelCreating(modelBuilder);
    }
}