using AutoMapper;
using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;
using E_commerce.Dtos;
using E_commerce.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
   
    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            return basket ?? new CustomerBasket(basketId);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdatedBasket = await _basketRepo.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);
        }

        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBaskets(string basketId)
        {
           return await _basketRepo.DeleteBasketAsync(basketId);
        }
    }
}
