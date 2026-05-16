using Microsoft.AspNetCore.Mvc;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) => _authService = authService;

        [HttpGet] public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            // Înregistrăm utilizatorul cu rolul de "User" implicit
            var result = await _authService.RegisterAsync(email, password, "User");
            if (result.Succeeded) return RedirectToAction("Login");
            return View();
        }

        [HttpGet] public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            var result = await _authService.LoginAsync(email, password, rememberMe);
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            ModelState.AddModelError("", "Login eșuat.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}