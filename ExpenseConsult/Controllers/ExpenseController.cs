using ExpenseConsult.Models;
using ExpenseConsult.Models.DTO;
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
    public async Task<IActionResult> CreateExpense([FromBody] ExpenseDto expenseDto)
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

        await _expenseRepository.AddAsync(expense);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDto expenseDto)
    {
        var expense = new Expense
        {
            Amount = expenseDto.Amount,
            Description = expenseDto.Description,
            CategoryId = expenseDto.CategoryId,
            UserId = expenseDto.UserId
        };

        await _expenseRepository.UpdateAsync(id,expense);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        await _expenseRepository.DeleteAsync(id);

        return Ok(); 
    }
}
