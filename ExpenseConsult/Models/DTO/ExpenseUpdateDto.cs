using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO
{
    public class ExpenseUpdateDto
    {
        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
