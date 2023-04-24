﻿using Azure;
using Marketplace.Domain.Entity;
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
        public async Task<IActionResult> SaveAuction()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAuction(ProductViewModel viewModel)
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
                    viewModel.isAuction = "true";
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
/*            var user1 = await _userService.GetUser(User.Identity.Name);
            var user2 = await _userService.GetUser(response.Data.OwnerName);*/
            var messages = await _chatMessageService.GetMessages(id.ToString(), User.Identity.Name, response.Data.OwnerName);

            if (messages.Data == null)
            {
                await _chatMessageService.GetOrCreateChat(id.ToString(), User.Identity.Name, response.Data.OwnerName, null);
                messages = await _chatMessageService.GetMessages(id.ToString(), User.Identity.Name, response.Data.OwnerName);
            }

            response.Data.ChatMessages = messages.Data;

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
