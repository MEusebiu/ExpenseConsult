using ExpenseDataAccessLayer.Models;

namespace ExpenseDataAccessLayer.Interfaces;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string categoryId);
    Task<IEnumerable<Expense>> GetExpensesAmountInterval(decimal minAmount, decimal maxAmount);
    Task<IEnumerable<Expense>> GetExpensesDatesInterval(DateTime minDate, DateTime maxDate);
}