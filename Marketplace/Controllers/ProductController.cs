using Marketplace.Domain.ViewModels.Product;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var response = _productService.GetProducts();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
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
                    await _productService.Create(viewModel, imageData);
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

        [HttpPost]
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
