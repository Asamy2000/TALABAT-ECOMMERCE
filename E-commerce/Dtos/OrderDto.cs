namespace E_commerce.Dtos
{
    public class OrderDto
    {
        public int deliveryMethodId { get; set; }

        public string basketId { get; set; }

        public AddressDto shippingAddress { get; set; }
    }
}
