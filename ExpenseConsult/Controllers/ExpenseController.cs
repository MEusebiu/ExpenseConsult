using ExpenseConsult.Models;
using ExpenseConsult.Models.DTO;
using ExpenseConsult.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var expenses = await _expenseService.GetExpensesAsync();

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(string id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreateDto expenseDto)
    {
        var expense = new Expense
        {
            Id = Guid.NewGuid().ToString(),
            Amount = expenseDto.Amount,
            Description = expenseDto.Description,
            CategoryId = expenseDto.CategoryId.ToString(),
            UserId = expenseDto.UserId.ToString()
        };
       
        await _expenseService.AddExpenseAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(string id, [FromBody] ExpenseUpdateDto expenseDto)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);

        expense.Amount = expenseDto.Amount;
        expense.Description = expenseDto.Description;

        await _expenseService.UpdateExpenseAsync(id, expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(string id)
    {
        await _expenseService.DeleteExpenseAsync(id);

        return Ok(); 
    }
}
