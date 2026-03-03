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
        public IActionResult Register(string firstName, string lastName, string password)
        {
            // Ruční validace
            if (string.IsNullOrEmpty(firstName))
            {
                ViewBag.Error = "Jméno je povinné";
                ViewBag.FirstName = firstName;
                ViewBag.LastName = lastName;
                return View();
            }

            if (string.IsNullOrEmpty(lastName))
            {
                ViewBag.Error = "Příjmení je povinné";
                ViewBag.FirstName = firstName;
                ViewBag.LastName = lastName;
                return View();
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                ViewBag.Error = "Heslo musí mít alespoň 6 znaků";
                ViewBag.FirstName = firstName;
                ViewBag.LastName = lastName;
                return View();
            }

            TempData["FirstName"] = firstName;
            TempData["LastName"] = lastName;
            TempData["Password"] = password;

            return RedirectToAction("Profil");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Ruční validace
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Error = "Uživatelské jméno je povinné";
                ViewBag.Username = username;
                return View("UserLogin");
            }

            if (string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Heslo je povinné";
                ViewBag.Username = username;
                return View("UserLogin");
            }

            // Zde by byla logika pro ověření přihlášení
            TempData["FirstName"] = username;
            TempData["LastName"] = "Random prijmeni";
            TempData["Password"] = password;

            return RedirectToAction("Profil");
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
