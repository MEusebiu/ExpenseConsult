using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseConsult.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
