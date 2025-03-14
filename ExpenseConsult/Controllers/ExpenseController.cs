using ExpenseConsult.Models;
using ExpenseConsult.Services;
using ExpenseConsult.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
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

        await _expenseService.AddExpenseAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest("Expense id not found");
        }

        await _expenseService.UpdateExpenseAsync(expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expenseService.DeleteExpenseAsync(id);

        return Ok(); 
    }
}
