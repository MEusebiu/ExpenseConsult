using ExpenseConsult.Models;
using ExpenseConsult.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private IRepository<int, Expense> _expenseRepository;

    public ExpenseController(IRepository<int, Expense> expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var expenses = await _expenseRepository.GetAllAsync();

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
    {
        if (expense == null)
        {
            return BadRequest("Expense data is required.");
        }

        await _expenseRepository.AddAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
    {
        await _expenseRepository.UpdateAsync(expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expenseRepository.DeleteAsync(id);

        return Ok(); 
    }
}
