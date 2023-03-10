using Azure;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Service.Implementations;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetProducts(string searchString)
        {
            var response = _productService.GetProducts();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (String.IsNullOrEmpty(searchString))
                {
                    return View(response.Data);
                }
                else
                {
                    return View(response.Data.Where(s => s.Name!.ToLower().Contains(searchString.ToLower())).ToList());
                }
            }

            return View("Error", $"{response.Description}");

        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id, User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts");
            }
            return View("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel viewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(viewModel.Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)viewModel.Photo.Length);
                    }
                    await _productService.Create(viewModel, imageData, User.Identity.Name);
                }
                else
                {
                    await _productService.Edit(viewModel.Id, viewModel);                    
                }
            }
            return RedirectToAction("GetProducts");
        }


        [HttpGet]
        public async Task<ActionResult> GetProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetProduct", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct(string term)
        {
            var response = await _productService.GetProduct(term);
            return Json(response.Data);
        }

        [HttpGet]
        public JsonResult GetCategory()
        {
            var types = _productService.GetCategory();
            return Json(types.Data);
        }

        [HttpPost]
        public JsonResult GetSubCategory()
        {
            var types = _productService.GetSubCategory();
            return Json(types.Data);
        }
    }
}
