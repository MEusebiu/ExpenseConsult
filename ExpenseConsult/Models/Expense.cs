using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseConsult.Models;

public class Expense
{
    [Key][JsonIgnore]
    public int Id { get; set; }

    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public int CategoryId { get; set; }

    [JsonIgnore][ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public int UserId { get; set; }

    [JsonIgnore][ForeignKey("UserId")]
    public virtual User User { get; set; }
}
