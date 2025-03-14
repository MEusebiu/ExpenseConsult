using ExpenseConsult.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseConsult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private Budget Budget { get; set; } = new();

    // GET: api/expenses/{id}
    [HttpGet("{id}")]
    public IActionResult GetExpense(int id)
    {
        var expense = Budget.Expenses.FirstOrDefault(p => p.Id == id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

    // POST: api/expenses/{id}
    [HttpPost]
    public IActionResult CreateExpense([FromBody] Expense expense)
    {
        if (expense == null)
        {
            return BadRequest("Expense data is required.");
        }

        expense.Id = Budget.Expenses.Count + 1;
        Budget.Expenses.Add(expense);

        return Ok();
    }
}
