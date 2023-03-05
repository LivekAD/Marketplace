using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

    }

}