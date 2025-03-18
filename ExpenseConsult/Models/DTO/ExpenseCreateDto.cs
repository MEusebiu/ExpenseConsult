using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO
{
    public class ExpenseCreateDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Description { get; set; }
    }
}
