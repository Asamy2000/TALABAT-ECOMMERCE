using AutoMapper;
using E_commerce.Core;
using E_commerce.Core.Entities;
using E_commerce.Core.IRepositories;
using E_commerce.Core.Specification;
using E_commerce.Dtos;
using E_commerce.Errors;
using E_commerce.Helper;

using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        //private readonly IGenericRepo<Product> _productRepo;
        //private readonly IGenericRepo<ProductBrand> _brandsRepo;
        //private readonly IGenericRepo<ProductType> _typesRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(/*IGenericRepo<Product> productRepo,IGenericRepo<ProductBrand> brandsRepo,IGenericRepo<ProductType> typesRepo,*/
            IMapper mapper ,IUnitOfWork unitOfWork)
        {
            //_productRepo = productRepo;
            //_brandsRepo = brandsRepo;
            //_typesRepo = typesRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Cahed(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]productSpecparams productSpecparams)
        {
            var productRepo = _unitOfWork.Repository<Product>();

            if (productRepo == null) return NotFound(new ApiResponse(404));

            var spec = new ProductWithBrandAndTypeSpecification(productSpecparams);

            var products = await productRepo.GetAllWithSpecAsync(spec);
            
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFiltirationForCountSpecs(productSpecparams);
            
            var count = await productRepo.GetCountWithSpecAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productSpecparams.PageIndex, productSpecparams.PageZize,count, data));
        }


        [Cahed(600)]
        /*improving swagger documentation if error or not*/
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var productRepo = _unitOfWork.Repository<Product>();

            if (productRepo == null) return NotFound(new ApiResponse(404));

            var spec = new ProductWithBrandAndTypeSpecification(id);

            var product = await productRepo.GetEntityWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }



        [Cahed(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBradns()
        {
            var brandRepo = _unitOfWork.Repository<ProductBrand>();

            if (brandRepo == null) return NotFound(new ApiResponse(404));

            var brands = await brandRepo.GetAllAsync();

            return Ok(brands);

        }


        [Cahed(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var typesRepo = _unitOfWork.Repository<ProductType>();

            if (typesRepo == null) return NotFound(new ApiResponse(404));

            var types = await typesRepo.GetAllAsync();

            return Ok(types);
        }

    }
}
