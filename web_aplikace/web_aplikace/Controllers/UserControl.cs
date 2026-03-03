using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web_aplikace.Models;

namespace web_aplikace.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            if (ModelState.IsValid)
            {
                TempData["FirstName"] = user.FirstName;
                TempData["LastName"] = user.LastName;
                TempData["Password"] = user.Password;

                return RedirectToAction("Profil");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Profil()
        {
            ViewBag.FirstName = TempData["FirstName"]?.ToString() ?? "Neuvedeno";
            ViewBag.LastName = TempData["LastName"]?.ToString() ?? "Neuvedeno";
            ViewBag.Password = TempData["Password"]?.ToString() ?? "Neuvedeno";

            return View("UserProfil");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
