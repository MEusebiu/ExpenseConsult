using System.ComponentModel.DataAnnotations;

namespace ExpenseConsult.Models.DTO
{
    public class ExpenseCreateDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Description { get; set; }
    }
}
