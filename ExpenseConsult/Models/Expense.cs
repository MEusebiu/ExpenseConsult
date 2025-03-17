using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseConsult.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }

    public decimal Amount { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
