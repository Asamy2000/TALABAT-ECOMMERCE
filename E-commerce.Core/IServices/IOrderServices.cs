using E_commerce.Core.Entities.Order_Aggregate;


namespace E_commerce.Core.IServices
{
    public interface IOrderServices
    {
        Task<Order?> CreateOrderAsync(string buyerEmail , string baskedId , int deliveryMethodId , Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
