using System.Text.Json.Serialization;

namespace ExpenseConsult.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
}
