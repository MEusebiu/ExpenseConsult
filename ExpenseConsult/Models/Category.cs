using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExpenseConsult.Models
{
    public class Category
    {
        [Key][JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
