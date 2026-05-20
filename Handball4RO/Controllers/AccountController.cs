using Handball4RO.Models;
using Handball4RO.Services;
using Microsoft.AspNetCore.Mvc;

namespace Handball4RO.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) => _authService = authService;

        [HttpGet] public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model.Email, model.Password, "User");

                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail")
                    {
                        ModelState.AddModelError(string.Empty, "Acest email este deja folosit de un alt cont.");
                    }
                    else if (error.Code.Contains("PasswordRequires") || error.Code == "PasswordTooShort")
                    {
                        ModelState.AddModelError(string.Empty, "Parola trebuie să aibă minim 6 caractere și să conțină o majusculă, o cifră și un caracter special (ex: @, #, !).");
                        break;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
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