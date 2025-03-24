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

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string categoryId)
    {
        var allExpenses = await _repository.GetAllAsync();
        var categoryExpenses = allExpenses.Where(e => e.CategoryId == categoryId);
        return categoryExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesAmountInterval(decimal minAmount, decimal maxAmount)
    {
        var allExpenses = await _repository.GetAllAsync();
        var filteredExpenses = allExpenses.Where(e => e.Amount >= minAmount && e.Amount <= maxAmount);
        return filteredExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesDatesInterval(DateTime minDate, DateTime maxDate)
    {
        var allExpenses = await _repository.GetAllAsync();
        var filteredExpenses = allExpenses.Where(e => e.CreatedDate >= minDate && e.CreatedDate <= maxDate);
        return filteredExpenses;
    }
}