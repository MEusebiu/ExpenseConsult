using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
