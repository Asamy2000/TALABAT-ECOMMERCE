using E_commerce.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public string? PaymentIntentId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

        public List<BasketItemDto> Items { get; set; } 
    }
}
