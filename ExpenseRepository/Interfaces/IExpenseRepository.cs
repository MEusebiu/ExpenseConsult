using ExpenseDataAccessLayer.Models;

namespace ExpenseDataAccessLayer.Interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string userId, string categoryId);
    Task<IEnumerable<Expense>> GetExpensesAmountInterval(string userId, decimal minAmount, decimal maxAmount);
    Task<IEnumerable<Expense>> GetExpensesDatesInterval(string userId, DateTime minDate, DateTime maxDate);
}