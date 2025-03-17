using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
