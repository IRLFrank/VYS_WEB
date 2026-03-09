using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_aplikace.Data;
using web_aplikace.Models;
using BCrypt.Net;

namespace web_aplikace.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string password)
        {
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

            // Vytvoření přezdívky z jména a příjmení
            string prezdivka = $"{firstName.ToLower()}.{lastName.ToLower()}";

            // Kontrola, zda přezdívka již neexistuje
            var existujiciUzivatel = await _context.Uzivatele
                .FirstOrDefaultAsync(u => u.Prezdivka == prezdivka);

            if (existujiciUzivatel != null)
            {
                // Přidání čísla k přezdívce, pokud již existuje
                int cislo = 1;
                string novaPrezdivka = prezdivka;
                while (await _context.Uzivatele.AnyAsync(u => u.Prezdivka == novaPrezdivka))
                {
                    novaPrezdivka = $"{prezdivka}{cislo}";
                    cislo++;
                }
                prezdivka = novaPrezdivka;
            }

            // Hash hesla pomocí BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Vytvoření nového uživatele
            var uzivatel = new Uzivatel
            {
                Jmeno = firstName,
                Prijmeni = lastName,
                Prezdivka = prezdivka,
                Heslo = hashedPassword,
                DatumRegistrace = DateTime.Now
            };

            _context.Uzivatele.Add(uzivatel);
            await _context.SaveChangesAsync();

            // Automatické přihlášení po registraci
            HttpContext.Session.SetInt32("UserId", uzivatel.Id);

            ViewBag.Success = $"Registrace úspěšná! Vaše přezdívka: {prezdivka}";
            return RedirectToAction("Profil");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
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

            // Hledání uživatele podle přezdívky
            var uzivatel = await _context.Uzivatele
                .FirstOrDefaultAsync(u => u.Prezdivka == username);

            if (uzivatel == null)
            {
                ViewBag.Error = "Neplatné přihlašovací údaje";
                ViewBag.Username = username;
                return View("UserLogin");
            }

            // Ověření hesla pomocí BCrypt
            if (!BCrypt.Net.BCrypt.Verify(password, uzivatel.Heslo))
            {
                ViewBag.Error = "Neplatné přihlašovací údaje";
                ViewBag.Username = username;
                return View("UserLogin");
            }

            // Uložení přihlášení do Session
            HttpContext.Session.SetInt32("UserId", uzivatel.Id);

            return RedirectToAction("Profil");
        }

        public async Task<IActionResult> Profil()
        {
            // Kontrola, zda je uživatel přihlášen
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            // Načtení dat uživatele z databáze
            var uzivatel = await _context.Uzivatele.FindAsync(userId);
            if (uzivatel == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }

            // Předání dat do View
            ViewBag.FirstName = uzivatel.Jmeno;
            ViewBag.LastName = uzivatel.Prijmeni;
            ViewBag.Username = uzivatel.Prezdivka;
            ViewBag.RegistrationDate = uzivatel.DatumRegistrace.ToString("dd.MM.yyyy");
            ViewBag.Password = "********"; // Nikdy nezobrazovat skutečné heslo!

            return View("UserProfil");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
