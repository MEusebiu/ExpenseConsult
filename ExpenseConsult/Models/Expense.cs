using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseConsult.Models;

public class Expense
{
    [Key]
    public string Id { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public string CategoryId { get; set; }

    [JsonIgnore]
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public string UserId { get; set; }

    [JsonIgnore]
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
