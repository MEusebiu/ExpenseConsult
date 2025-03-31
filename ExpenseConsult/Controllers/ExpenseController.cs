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
    private ILogger<CategoryController> _logger;

    public ExpenseController(IExpenseService expenseService, ILogger<CategoryController> logger)
    {
        _expenseService = expenseService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetUserExpensesAsync(userId);

        _logger.LogInformation("Successfully fetched {Count} expenses", expenses.ToList().Count);

        return Ok(expenses);
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

        _logger.LogInformation("Successfully fetched {expenseName}", expense.Description);

        return Ok(expense);
    }

    [HttpGet("filter-by-category")]
    public async Task<IActionResult> GetExpensesByCategory(string categoryId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetExpensesByCategoryAsync(userId, categoryId);

        _logger.LogInformation("Successfully fetched {Count} expenses for selected category", expenses.ToList().Count);

        return Ok(expenses);
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

        _logger.LogInformation("Successfully added {expenseName}", expense.Description);

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

        _logger.LogInformation("Successfully updated {expenseName}", expense.Description);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(string id)
    {
        await _expenseService.DeleteExpenseAsync(id);

        _logger.LogInformation("Successfully deleted expense");

        return Ok(); 
    }

    [HttpGet("filter-by-amount")]
    public async Task<IActionResult> GetExpensesInAmountRange([FromQuery] decimal minAmount, [FromQuery] decimal maxAmount)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetExpensesAmountInterval(userId, minAmount, maxAmount);

        _logger.LogInformation("Successfully fetched {Count} expenses for selected amount range", expenses.ToList().Count);

        return Ok(expenses);
    }

    [HttpGet("filter-by-date")]
    public async Task<IActionResult> GetExpensesInDateRange([FromQuery] string minDate, [FromQuery] string maxDate)
    {
        if (!DateTime.TryParse(minDate, out var minDateParsed) || !DateTime.TryParse(maxDate, out var maxDateParsed))
        {
            return BadRequest("Invalid date format");
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var expenses = await _expenseService.GetExpensesDatesInterval(userId, minDateParsed, maxDateParsed);

        _logger.LogInformation("Successfully fetched {Count} expenses for selected date range", expenses.ToList().Count);

        return Ok(expenses);
    }
}
