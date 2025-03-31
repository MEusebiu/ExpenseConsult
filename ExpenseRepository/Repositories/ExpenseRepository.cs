using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

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
        var result = await GetExpensesWithDetails();

        var categoryExpenses = result.Where(e => e.CategoryId == categoryId && e.UserId == userId);
        return categoryExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesAmountInterval(string userId, decimal minAmount, decimal maxAmount )
    {
        var result = await GetExpensesWithDetails();

        var filteredExpenses = result.Where(e => e.UserId == userId && e.Amount >= minAmount && e.Amount <= maxAmount);
        return filteredExpenses;
    }

    public async Task<IEnumerable<Expense>> GetExpensesDatesInterval(string userId, DateTime minDate, DateTime maxDate)
    {
        var result = await GetExpensesWithDetails();

        var filteredExpenses = result.Where(e => e.UserId == userId && e.CreatedDate >= minDate && e.CreatedDate <= maxDate);
        return filteredExpenses;
    }

    private async Task<IEnumerable<Expense>> GetExpensesWithDetails()
    {
        var expenses = await _repository.GetAllAsync(query =>
            query.Include(e => e.User)
                .Include(f => f.Category));

        var result = expenses.Select((e => new Expense
        {
            Id = e.Id,
            Description = e.Description,
            Amount = e.Amount,
            CreatedDate = e.CreatedDate,
            UserId = e.UserId,
            UserName = e.User.UserName,
            CategoryId = e.Category.Id,
            CategoryName = e.Category.Name
        }));

        return result;
    }
}