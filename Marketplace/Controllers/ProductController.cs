using Azure;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Enum;
using Marketplace.Domain.Extensions;
using Marketplace.Domain.ViewModels.Account;
using Marketplace.Domain.ViewModels.Chat;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Service.Implementations;
using Marketplace.Service.Interfaces;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Marketplace.Controllers
{
    public class ProductController : Controller
    {
        #region Include Data Base Service

        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IChatMessageService _chatMessageService;

        public ProductController(IProductService productService, IUserService userService, IChatMessageService chatMessageService)
        {
            _productService = productService;
            _userService = userService;
            _chatMessageService = chatMessageService;
        }

        #endregion

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

        #region Save Product

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
                    List<ProductPhoto> photos = new List<ProductPhoto>();
                    foreach (var photo in viewModel.Photo)
                    {
                        using (var binaryReader = new BinaryReader(photo.OpenReadStream()))
                        {
                            byte[] imageData = binaryReader.ReadBytes((int)photo.Length);
                            photos.Add(new ProductPhoto { ImageData = imageData });
                        }
                    }
                    if (viewModel.EndingAuction != null){
                        viewModel.isAuction = "true";
                    }
                    await _productService.Create(viewModel, photos, User.Identity.Name);
                }
                else
                {
                    await _productService.Edit(viewModel.Id, viewModel);                    
                }
            }
            return RedirectToAction("GetProducts");
        }

        #endregion

        #region Get Product

        [HttpGet]
        public async Task<ActionResult> GetProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            if (User.Identity.Name != null && response.Data.OwnerName != User.Identity.Name)
            {
				var messages = await _chatMessageService.GetMessages(id.ToString(), User.Identity.Name, response.Data.OwnerName);

				if (messages.Data == null)
				{
					await _chatMessageService.CreateChat(id.ToString(), User.Identity.Name, response.Data.OwnerName);
					messages = await _chatMessageService.GetMessages(id.ToString(), User.Identity.Name, response.Data.OwnerName);
				}

				response.Data.ChatMessages = messages.Data;
			}           

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

        #endregion

        #region Get Auction

        [HttpGet]
        public async Task<ActionResult> GetAuctionProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetAuctionProduct", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetAuctionProduct(string term)
        {
            var response = await _productService.GetProduct(term);
            return Json(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddBid(Bid bid)
        {

            if (ModelState.IsValid)
            {
                var response = await _productService.AddBid(bid, User.Identity.Name);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        }

        #endregion

        #region Category/SubCategory

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

        [HttpGet]
        public IActionResult GetProductsByCategory(string category)
        {
            var response = _productService.GetProductsByCategory(category);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("GetProducts", response.Data);
            }

            return View("Error", $"{response.Description}");

        }

        [HttpPost]
        public JsonResult GetSubcategories(int categoryId)
        {
            var subcategories = Enum.GetValues(typeof(SubCategory))
                .Cast<SubCategory>()
                .Where(s => (((int)s - 4000) / 10) == categoryId)
                .Select(s => new { Id = (int)s, Name = s.GetDisplayName() })
                .ToList();

            return Json(subcategories);
        }

        #endregion

        /*[HttpPost]
        public async Task<IActionResult> Chat(int id)
        {
            var response = await _productService.GetProduct(id);
            var user1 = await _userService.GetUser(User.Identity.Name);
            var user2 = await _userService.GetUser(response.Data.OwnerName);
            var messages = await _chatMessageService.GetMessages(id.ToString(), user1.Data.Id.ToString(), user2.Data.Id.ToString());

            if(messages.Data == null)
            {
                await _chatMessageService.GetOrCreateChat(id.ToString(), user1.Data.Id.ToString(), user2.Data.Id.ToString(), null);
                messages = await _chatMessageService.GetMessages(id.ToString(), user1.Data.Id.ToString(), user2.Data.Id.ToString());
            }            

            if (messages.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Json(messages.Data);
            }
            return RedirectToAction("GetProducts", "Product");           
        }*/

        
    }
}
