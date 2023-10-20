using E_commerce.Core;
using E_commerce.Core.Entities;
using E_commerce.Core.Entities.Order_Aggregate;
using E_commerce.Core.IRepositories;
using E_commerce.Core.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Product = E_commerce.Core.Entities.Product;

namespace E_commerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepo basketRepo, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            // Fetch the basket details from _basketRepo 
            var basket = await _basketRepo.GetBasketAsync(basketId);

            if (basket is null) return null;


            //Shipping Price
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetBYIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod.Cost;
                shippingPrice = deliveryMethod.Cost;
            }

            //get added Products
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    //get basket items from Product Repo
                    var product = await _unitOfWork.Repository<Product>().GetBYIdAsync(item.Id);
                    //validate items Price
                    if (item.price != product.Price)
                        item.price = product.Price;
                }

            }



            // Create or update a payment intent with Stripe
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //creation for paymentIntent 
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * item.quantity * 100) + (long)shippingPrice * 100, // Amount in cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Update PaymentIntent
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.price * item.quantity * 100) + (long)shippingPrice * 100, // Amount in cents

                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepo.UpdateBasketAsync(basket);

            return basket;
        }
    }
}
