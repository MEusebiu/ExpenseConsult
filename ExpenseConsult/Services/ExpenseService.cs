using ExpenseConsult.Models;
using ExpenseConsult.Repositories;

namespace ExpenseConsult.Services
{
    public class ExpenseService
    {
        private readonly IRepository<Expense> _expenseRepository;

        public ExpenseService(IRepository<Expense> expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _expenseRepository.AddAsync(expense);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            await _expenseRepository.UpdateAsync(expense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            await _expenseRepository.DeleteAsync(id);
        }
    }
}
