using ExpenseConsult.Models;

namespace ExpenseConsult.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpensesAsync();

        Task<Expense> GetExpenseByIdAsync(int id);

        Task AddExpenseAsync(Expense expense);

        Task UpdateExpenseAsync(Expense expense);

        Task DeleteExpenseAsync(int id);
    }
}
