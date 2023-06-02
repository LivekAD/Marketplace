using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
	public class ChatController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
