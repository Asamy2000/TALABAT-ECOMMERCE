using AdminPanel.Models;
using E_commerce.Core;
using E_commerce.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
           var allBrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return View(allBrands);
        }

        public async Task<IActionResult> Create(BrandFormVM model)
        {
            if(model.Name is not null)
            {
               await _unitOfWork.Repository<ProductBrand>().Add(new ProductBrand(model.Name));
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id )
        {
            var brand = await _unitOfWork.Repository<ProductBrand>().GetBYIdAsync(id);
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductBrand model)
        {
            try
            {
                if (model.Name is not null)
                {
                    // Check if a brand with the same name already exists
                    var existingBrand = await _unitOfWork.Repository<ProductBrand>().GetBYNameAsync(model.Name);
                    if (existingBrand != null)
                    {
                        ModelState.AddModelError("Name", "The Brand already exists");
                        return View(model);
                    }

                    // Update the brand 
                    _unitOfWork.Repository<ProductBrand>().Update(model);
                    await _unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Brand name is required.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Handle and log exceptions appropriately
                // You may want to log the exception details, redirect to an error page, or take other action
                ModelState.AddModelError("Error", "An error occurred while saving the brand.");
                return View(model);
            }
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.Repository<ProductBrand>().GetBYIdAsync(id);
            _unitOfWork.Repository<ProductBrand>().Delete(brand);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }



    }
}
