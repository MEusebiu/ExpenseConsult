using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseConsult.Models;

public class User
{
    [Key][JsonIgnore]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [JsonIgnore]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
