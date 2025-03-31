using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseServices.Services;

public class ExpenseService : IExpenseService
{
    private readonly IRepository<string, Expense> _repository;
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IRepository<string, Expense> repository, IExpenseRepository expenseRepository)
    {
        _repository = repository;
        _expenseRepository = expenseRepository;
    }

    public async Task<IEnumerable<Expense>> GetUserExpensesAsync(string userId)
    {
        var expenses =
            await _repository.GetAllAsync(query =>
                query.Include(a => a.User)
                     .Include(e => e.Category));
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

        return result.Where(e => e.UserId == userId);
    }

    public async Task<Expense> GetExpenseByIdAsync(string userId, string id)
    {
        var expense = await _repository.GetByIdAsync(id, query => 
            query.Include(a => a.User)
                 .Include(e => e.Category));

        var result = new Expense
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            CreatedDate = expense.CreatedDate,
            UserId = expense.UserId,
            UserName = expense.User.UserName,
            CategoryId = expense.Category.Id,
            CategoryName = expense.Category.Name
        };

        return result.UserId == userId ? expense : null;
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        await _repository.AddAsync(expense);
    }

    public async Task UpdateExpenseAsync(string key, Expense expense)
    {
        await _repository.UpdateAsync(key, expense);
    }

    public async Task DeleteExpenseAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string userId, string categoryId)
    {
        return await _expenseRepository.GetExpensesByCategoryAsync(userId, categoryId);
    }

    public async Task<IEnumerable<Expense>> GetExpensesAmountInterval(string userId, decimal min, decimal max)
    {
        return await _expenseRepository.GetExpensesAmountInterval(userId, min, max);
    }

    public async Task<IEnumerable<Expense>> GetExpensesDatesInterval(string userId, DateTime minDate, DateTime maxDate)
    {
        return await _expenseRepository.GetExpensesDatesInterval(userId, minDate, maxDate);
    }
}
