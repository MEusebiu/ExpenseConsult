using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApi.Models.DTO;

public class ExpenseUpdateDto
{
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }

    public string Description { get; set; }
}
