using Azure;
using Marketplace.Domain.Entity;
using Marketplace.Domain.ViewModels.Chat;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace Marketplace.Controllers
{
	public class ChatController : Controller
	{
		#region Include Data Base Service

		private readonly IProductService _productService;
		private readonly IUserService _userService;
		private readonly IChatMessageService _chatMessageService;

		public ChatController(IProductService productService, IUserService userService, IChatMessageService chatMessageService)
		{
			_productService = productService;
			_userService = userService;
			_chatMessageService = chatMessageService;
		}

		#endregion

		[HttpGet]

		public async Task<IActionResult> Index()
		{
			var chats = await _chatMessageService.GetChats(User.Identity.Name);
			var product = _productService.GetProducts().Data.ToList();

			List<Product> productsort = new List<Product> { };
			HashSet<string> uniqueProducts = new HashSet<string>();

			string check = "";

			foreach (var chat in chats.Data) {
				if(productsort == null || !uniqueProducts.Contains(chat.ProductId))
				{
					productsort.AddRange(product.Where(x => x.Id.ToString() == chat.ProductId));
					uniqueProducts.Add(chat.ProductId);
				}
				//check = chat.ProductId;
			}

			//var products = _productService.GetProducts().Data.Where(x => x.Id.ToString() == productsId.).ToList();
			
			var response = new ChatMessageViewModel
			{
				Chats = chats.Data,
				Products = productsort
			};

			if (chats.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return View(response);
			}

			return View("Error", $"{chats.Description}");
		}

		[HttpGet]

		public async Task<IActionResult> Chat(int productId)
		{
			var response = await _productService.GetProduct(productId);
			if (User.Identity.Name != null && response.Data.OwnerName != User.Identity.Name)
			{
				var messages = await _chatMessageService.GetMessages(productId.ToString(), User.Identity.Name, response.Data.OwnerName);

				if (messages.Data == null)
				{
					await _chatMessageService.CreateChat(productId.ToString(), User.Identity.Name, response.Data.OwnerName);
					messages = await _chatMessageService.GetMessages(productId.ToString(), User.Identity.Name, response.Data.OwnerName);
				}

				response.Data.ChatMessages = messages.Data;
			}

			return PartialView("Chat", response.Data);
		}
	}
}
