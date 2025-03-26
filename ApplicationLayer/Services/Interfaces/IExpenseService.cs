using ExpenseDataAccessLayer.Models;

namespace ExpenseServices.Services.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetUserExpensesAsync(string userId);
    Task<Expense> GetExpenseByIdAsync(string userId, string id);
    Task AddExpenseAsync(Expense expense);
    Task UpdateExpenseAsync(string id, Expense expense);
    Task DeleteExpenseAsync(string id);
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string userId, string categoryId);
    Task<IEnumerable<Expense>> GetExpensesAmountInterval(string userId, decimal minAmount, decimal maxAmount);
    Task<IEnumerable<Expense>> GetExpensesDatesInterval(string userId, DateTime minDate, DateTime maxDate);
}
