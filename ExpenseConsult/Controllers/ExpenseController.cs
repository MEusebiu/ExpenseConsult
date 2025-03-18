using ExpenseConsult.Models;
using ExpenseConsult.Models.DTO;
using ExpenseConsult.Repositories;
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

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var expenses = await _expenseService.GetExpensesAsync();

        return Ok(expenses);
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
    public async Task<IActionResult> CreateExpense([FromBody] ExpenseCreateDto expenseDto)
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is required.");
        }

        var expense = new Expense
        {
            Amount = expenseDto.Amount,
            Description = expenseDto.Description,
            CategoryId = expenseDto.CategoryId,
            UserId = expenseDto.UserId
        };
       
        await _expenseService.AddExpenseAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseCreateDto expenseDto)
    {
        var expense = new Expense
        {
            Amount = expenseDto.Amount,
            Description = expenseDto.Description,
            CategoryId = expenseDto.CategoryId,
            UserId = expenseDto.UserId
        };

        await _expenseService.UpdateExpenseAsync(id,expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expenseService.DeleteExpenseAsync(id);

        return Ok(); 
    }
}
