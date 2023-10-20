using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using E_commerce.Core;
using E_commerce.Core.Entities;
using E_commerce.Core.Specification;
using E_commerce.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            
            var allProdcts = await _unitOfWork.Repository<Product>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductVM>>(allProdcts);
            return View(mappedProducts);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductVM model)
        {
            if (ModelState.IsValid) 
            {
                if (model.Image is not null)
                    model.PicUrl = FileSettings.UploadFile(model.Image, "products");

                var mappedProduct = _mapper.Map<ProductVM, Product>(model);
                await _unitOfWork.Repository<Product>().Add(mappedProduct);
                int count = await _unitOfWork.Complete();
                if (count > 0)
                    TempData["Message"] = "Product created successfuly";

                return RedirectToAction(nameof(Index));
            }
            return View(model);


        }

        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var productToEdit = await _unitOfWork.Repository<Product>().GetBYIdAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductVM>(productToEdit);
            return View(mappedProduct);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id,ProductVM model)
        {
            if(id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                {
                    if(model.PicUrl is not null)
                    {
                        FileSettings.DeleteFile(model.PicUrl,"products");
                        model.PicUrl = FileSettings.UploadFile(model.Image,"products");
                    }
                    else
                        model.PicUrl = FileSettings.UploadFile(model.Image, "products");

                }

                var mappedProduct = _mapper.Map<ProductVM, Product>(model);
                _unitOfWork.Repository<Product>().Update(mappedProduct);
                var result = await _unitOfWork.Complete();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }

            return View(model);

        }




        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var productToDelete = await _unitOfWork.Repository<Product>().GetBYIdAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductVM>(productToDelete);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, ProductVM model)
        {
            if (id != model.Id)
                return BadRequest();
            try
            {
                var mappedProduct = _mapper.Map<ProductVM, Product>(model);
                _unitOfWork.Repository<Product>().Delete(mappedProduct);
                int Count = await _unitOfWork.Complete();
                if (Count > 0 && model.PicUrl is not null)
                {
                    FileSettings.DeleteFile(model.PicUrl, "products");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

        }
    }
}
