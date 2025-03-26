using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;

namespace ExpenseDataAccessLayer.Repositories;

class ExpenseRepository : IExpenseRepository
{
    private IRepository<string, Expense> _repository;

    public ExpenseRepository(IRepository<string, Expense> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string userId, string categoryId)
    {
        var allExpenses = await _repository.GetAllAsync();
        var categoryExpenses = allExpenses.Where(e => e.CategoryId == categoryId && e.UserId == userId);
        return categoryExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesAmountInterval(string userId, decimal minAmount, decimal maxAmount )
    {
        var allExpenses = await _repository.GetAllAsync();
        var filteredExpenses = allExpenses.Where(e => e.UserId == userId && e.Amount >= minAmount && e.Amount <= maxAmount);
        return filteredExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesDatesInterval(string userId, DateTime minDate, DateTime maxDate)
    {
        var allExpenses = await _repository.GetAllAsync();
        var filteredExpenses = allExpenses.Where(e => e.UserId == userId && e.CreatedDate >= minDate && e.CreatedDate <= maxDate);
        return filteredExpenses;
    }
}