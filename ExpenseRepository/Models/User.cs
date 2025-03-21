using Microsoft.AspNetCore.Identity;

namespace ExpenseDataAccessLayer.Models;

public class User : IdentityUser
{
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
