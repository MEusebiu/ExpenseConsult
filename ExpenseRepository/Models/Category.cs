using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseDataAccessLayer.Models;

public class Category
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public virtual IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
}
