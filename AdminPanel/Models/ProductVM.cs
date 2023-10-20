using E_commerce.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile? Image { get; set; }

        public string? PicUrl { get; set; }

        [Required(ErrorMessage = "Price is Required !")]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Product Brand Id is Required !")]
        public int ProductBrandId { get; set; }

        public ProductBrand? Brand { get; set; }

        [Required(ErrorMessage = "Product Product Type Id is Required !")]
        public int ProductTypeId { get; set; }

        public ProductType? ProductType { get; set; }
    }
}
