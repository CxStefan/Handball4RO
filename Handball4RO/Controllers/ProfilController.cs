using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Handball4RO.Models;
using Handball4RO.Data;
using System.Security.Claims;

namespace Handball4RO.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfilController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");

            var user = await _context.Users
                .Include(u => u.EchipeFavorite).ThenInclude(ef => ef.Echipa)
                .Include(u => u.JucatoriFavoriti).ThenInclude(jf => jf.Jucator)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return RedirectToAction("Login", "Account");

            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Editeaza()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var model = new ProfilViewModel
            {
                NumeComplet = user.NumeComplet,
                PozaProfilUrl = user.PozaProfilUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(ProfilViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            user.NumeComplet = model.NumeComplet;
            user.PozaProfilUrl = model.PozaProfilUrl;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Mesaj"] = "Profilul a fost actualizat cu succes!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ToggleEchipaFavorita(int echipaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existaDeja = await _context.EchipeFavorite.FirstOrDefaultAsync(e => e.ApplicationUserId == userId && e.EchipaId == echipaId);

            if (existaDeja != null)
                _context.EchipeFavorite.Remove(existaDeja);
            else
                _context.EchipeFavorite.Add(new EchipaFavorita { ApplicationUserId = userId, EchipaId = echipaId });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Echipe");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleJucatorFavorit(int jucatorId, int echipaIdReturn)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existaDeja = await _context.JucatoriFavoriti.FirstOrDefaultAsync(j => j.ApplicationUserId == userId && j.JucatorId == jucatorId);

            if (existaDeja != null)
                _context.JucatoriFavoriti.Remove(existaDeja);
            else
                _context.JucatoriFavoriti.Add(new JucatorFavorit { ApplicationUserId = userId, JucatorId = jucatorId });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Jucatori", new { echipaId = echipaIdReturn });
        }
    }
}