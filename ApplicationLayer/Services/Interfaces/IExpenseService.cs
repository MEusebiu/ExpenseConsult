using ExpenseDataAccessLayer.Models;

namespace ExpenseServices.Services.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetExpensesAsync();

    Task<Expense> GetExpenseByIdAsync(string id);

    Task AddExpenseAsync(Expense expense);

    Task UpdateExpenseAsync(string id, Expense expense);

    Task DeleteExpenseAsync(string id);
}
