using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; } = default!;
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="price can't be zero")]
        public decimal price { get; set; }
        [Required]
        public string pictureUrl { get; set; } = default!;
        [Required]
        [Range (1, int.MaxValue,ErrorMessage ="quantity must be at least one item")]
        public int quantity { get; set; }
        [Required]
        public string brand { get; set; } = default!;
        [Required]
        public string type { get; set; } = default!;
    }
}