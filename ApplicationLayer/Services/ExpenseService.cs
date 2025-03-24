using ExpenseDataAccessLayer.Interfaces;
using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;

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

    public async Task<IEnumerable<Expense>> GetExpensesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Expense> GetExpenseByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
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

    public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string categoryId)
    {
        return await _expenseRepository.GetExpensesByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Expense>> GetExpensesAmountInterval(decimal min, decimal max)
    {
        return await _expenseRepository.GetExpensesAmountInterval(min, max);
    }

    public async Task<IEnumerable<Expense>> GetExpensesDatesInterval(DateTime minDate, DateTime maxDate)
    {
        return await _expenseRepository.GetExpensesDatesInterval(minDate, maxDate);
    }
}
