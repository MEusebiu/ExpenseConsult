using ExpenseDataAccessLayer.Models;
using ExpenseServices.Services.Interfaces;
using ExpenseWebApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseWebApi.Controllers;

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
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetUserExpensesAsync(userId);

        return Ok(expenses);
    }

    [HttpGet("filter-by-category")]
    public async Task<IActionResult> GetProductsByCategory(string categoryId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetExpensesByCategoryAsync(userId, categoryId);
        
        return Ok(expenses);
    }

    [HttpGet("filter-by-amount")]
    public async Task<IActionResult> GetExpensesInAmountRange([FromQuery] decimal minAmount, [FromQuery] decimal maxAmount)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var filteredExpenses = await _expenseService.GetExpensesAmountInterval(userId, minAmount, maxAmount);
        
        return Ok(filteredExpenses);
    }

    [HttpGet("filter-by-date")]
    public async Task<IActionResult> GetExpensesInDateRange([FromQuery] string minDate, [FromQuery] string maxDate)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!DateTime.TryParse(minDate, out var minDateParsed) || !DateTime.TryParse(maxDate, out var maxDateParsed))
        {
            return BadRequest("Invalid date format");
        }

        var filteredExpenses = await _expenseService.GetExpensesDatesInterval(userId, minDateParsed, maxDateParsed);
        return Ok(filteredExpenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expense = await _expenseService.GetExpenseByIdAsync(userId, id);
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
            CategoryId = expenseDto.CategoryId,
            UserId = expenseDto.UserId
        };
       
        await _expenseService.AddExpenseAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(string id, [FromBody] ExpenseUpdateDto expenseDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expense = await _expenseService.GetExpenseByIdAsync(userId, id);

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
