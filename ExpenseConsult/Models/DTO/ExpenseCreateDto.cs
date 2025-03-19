using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO;

public class ExpenseCreateDto
{
    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }
    public string Description { get; set; }

    [Required(ErrorMessage = "Category is required")]
    public string CategoryId { get; set; }

    [Required(ErrorMessage = "User is required")]
    public string UserId { get; set; }
}
