using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseDataAccessLayer.Models;

public class Expense
{
    [Key]
    public string Id { get; set; }

    public string Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }

    [JsonIgnore]
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    [JsonIgnore]
    public string UserId { get; set; }
    public string UserName { get; set; }

    [JsonIgnore]
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
