using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO;

public class CategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
