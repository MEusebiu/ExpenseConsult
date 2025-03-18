using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO
{
    public class UserUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
