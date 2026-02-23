using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_aplikace.Models;

namespace web_aplikace.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult hypertrophy()
        {
            return View();
        }

        public IActionResult overload()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
