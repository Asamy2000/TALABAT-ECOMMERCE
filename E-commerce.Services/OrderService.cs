using E_commerce.Core;
using E_commerce.Core.Entities;
using E_commerce.Core.Entities.Order_Aggregate;
using E_commerce.Core.IRepositories;
using E_commerce.Core.IServices;
using E_commerce.Core.Specification.OrderSpec;

namespace E_commerce.Services
{
    public class OrderService : IOrderServices
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepo<Product> _productRepo;
        //private readonly IGenericRepo<DeliveryMethod> _deliveryMethodsRepo;
        //private readonly IGenericRepo<Order> _orderRepo;

        public OrderService(IBasketRepo basketRepo,
              //IGenericRepo<Product> productRepo ,
              //IGenericRepo<DeliveryMethod> deliveryMethodRepo,
              //IGenericRepo<Order> orderRepo
              IUnitOfWork unitOfWork,
              IPaymentService paymentService
            )
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productRepo;
            //_deliveryMethodsRepo = deliveryMethodRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string baskedId, int deliveryMethodId, Address shippingAddress)
        {
            //1.Get Basket from Baskets Repo

            /*
              the basket is CustomerBasket which has two props 
               1.Id
               2.Items --> list of BasketItem  [iterated in the foreach in the second step ]
             
             */
            var basket = await _basketRepo.GetBasketAsync(baskedId);

            //2.Get selected Items in the Basket from Product Repo
            var orderItems = new List<OrderItem>(); //to contain the basket items

            //get the selected items  from the DataBase

            if (basket?.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var productRepo = _unitOfWork.Repository<Product>();

                    if (productRepo != null)
                    {
                        var product = await productRepo.GetBYIdAsync(item.Id);

                        if (product != null)
                        {
                            var productItemOrdered = new ProductItemOrderd(product.Id, product.Name, product.PicUrl);

                            var orderItem = new OrderItem(productItemOrdered, product.Price, item.quantity);

                            orderItems.Add(orderItem);


                        }

                    }


                }
            }


            //3.Calc SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //4.Get Delivery Method from DelevieryMethods Repo
            DeliveryMethod deliveryMethod = new DeliveryMethod();

            var deliveryMethodRepo = _unitOfWork.Repository<DeliveryMethod>();
            if (deliveryMethodRepo != null)
                deliveryMethod = await deliveryMethodRepo.GetBYIdAsync(deliveryMethodId);



            //5.Create Order
            var spec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentId);
            var OrderRepo = _unitOfWork.Repository<Order>();
            //6.save To DataBase [TODO]
            if (OrderRepo != null)
            {
                await OrderRepo.Add(order);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                    return order;
            }




            return null;

        }



        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var Orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return Orders;
        }



        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecification(buyerEmail, orderId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (order is null) return null;

            return order;

        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }
    }
}
