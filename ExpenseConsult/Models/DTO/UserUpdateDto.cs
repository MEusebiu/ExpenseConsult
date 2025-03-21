using System.ComponentModel.DataAnnotations;

namespace ExpenseWebApi.Models.DTO;

public class UserUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
