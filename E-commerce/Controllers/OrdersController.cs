using AutoMapper;
using E_commerce.Core.Entities.Order_Aggregate;
using E_commerce.Core.IServices;
using E_commerce.Dtos;
using E_commerce.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Order = E_commerce.Core.Entities.Order_Aggregate.Order;

namespace E_commerce.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices ,IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var mappedAddress = _mapper.Map<AddressDto, Address>(orderDto.shippingAddress);

            var order = await _orderServices.CreateOrderAsync(buyerEmail, orderDto.basketId, orderDto.deliveryMethodId, mappedAddress);

            if (order is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrdersForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto> >(orders));
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<OrderToReturnDto>> GetOrdersForUserById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderServices.GetOrderByIdForUserAsync(email, id);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }


        [HttpGet("deliveryMethods")] //api/Orders/deliveryMethods

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderServices.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
