using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using ExpenseConsult.Services.Interfaces;

namespace ExpenseConsult.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<string, Expense> _expenseRepository;

        public ExpenseService(IRepository<string, Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(string id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _expenseRepository.AddAsync(expense);
        }

        public async Task UpdateExpenseAsync(string key, Expense expense)
        {
            await _expenseRepository.UpdateAsync(key, expense);
        }

        public async Task DeleteExpenseAsync(string id)
        {
            await _expenseRepository.DeleteAsync(id);
        }
    }
}
