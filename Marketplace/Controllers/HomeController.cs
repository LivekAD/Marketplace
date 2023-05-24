using Marketplace.Models;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
		private readonly IProductService _productService;

		public HomeController(IProductService productService)
		{
			_productService = productService;
		}

		public IActionResult Index()
		{
			var response = _productService.GetProducts();

			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
					return View(response.Data);
			}

			return View("Error", $"{response.Description}");
		}

    }

}