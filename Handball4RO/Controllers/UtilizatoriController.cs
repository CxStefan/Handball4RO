using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Handball4RO.Models;

namespace Handball4RO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtilizatoriController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UtilizatoriController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var utilizatori = await _userManager.Users.ToListAsync();
            var listaViewModel = new List<UtilizatorViewModel>();

            foreach (var user in utilizatori)
            {
                var roles = await _userManager.GetRolesAsync(user);

                listaViewModel.Add(new UtilizatorViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    NumeComplet = user.NumeComplet,
                    RolCurent = roles.FirstOrDefault() ?? "User"
                });
            }

            return View(listaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SchimbaRol(string userId, string rolNou)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            if (user.Email == User.Identity.Name && rolNou != "Admin")
            {
                TempData["Eroare"] = "❌ Nu îți poți scoate singur gradul de Admin!";
                return RedirectToAction(nameof(Index));
            }

            var roluriCurente = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roluriCurente);

            if (!await _roleManager.RoleExistsAsync(rolNou))
            {
                await _roleManager.CreateAsync(new IdentityRole(rolNou));
            }

            await _userManager.AddToRoleAsync(user, rolNou);

            TempData["Mesaj"] = $"✅ Rolul pentru {user.Email} a fost schimbat în {rolNou}.";
            return RedirectToAction(nameof(Index));
        }
    }
}