using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO;

public class ExpenseUpdateDto
{
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }

    public string Description { get; set; }
}
