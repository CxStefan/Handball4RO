using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Services;
using Microsoft.AspNetCore.Authorization;

namespace Handball4RO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EchipeController : Controller
    {
        private readonly IEchipaService _echipaService;

        public EchipeController(IEchipaService echipaService)
        {
            _echipaService = echipaService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int? competitieId)
        {
            ViewBag.CompetitieId = competitieId;

            if (competitieId.HasValue)
            {
                var echipe = await _echipaService.ObtineEchipeDupaCompetitieAsync(competitieId.Value);
                return View(echipe);
            }

            var toateEchipele = await _echipaService.ObtineToateAsync();
            return View(toateEchipele);
        }

        public IActionResult Adauga(int? competitieId)
        {
            ViewBag.CompetitieId = competitieId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Echipa echipa, int? competitieId)
        {
            ModelState.Remove("Jucatori");
            ModelState.Remove("MeciuriAcasa");
            ModelState.Remove("MeciuriDeplasare");
            ModelState.Remove("Clasamente");

            if (ModelState.IsValid)
            {
                
                await _echipaService.AdaugaAsync(echipa, competitieId);

                return RedirectToAction(nameof(Index), new { competitieId = competitieId });
            }

            ViewBag.CompetitieId = competitieId;
            return View(echipa);
        }
        public async Task<IActionResult> Editeaza(int? id)
        {
            if (id == null) return NotFound();
            var echipa = await _echipaService.ObtineDupaIdAsync(id.Value);
            if (echipa == null) return NotFound();
            return View(echipa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(int id, Echipa echipa)
        {
            if (id != echipa.Id) return NotFound();

            ModelState.Remove("Jucatori");
            ModelState.Remove("MeciuriAcasa");
            ModelState.Remove("MeciuriDeplasare");
            ModelState.Remove("Clasamente");

            if (ModelState.IsValid)
            {
                await _echipaService.EditeazaAsync(echipa);
                return RedirectToAction(nameof(Index));
            }
            return View(echipa);
        }

        public async Task<IActionResult> Sterge(int? id)
        {
            if (id == null) return NotFound();
            var echipa = await _echipaService.ObtineDupaIdAsync(id.Value);
            if (echipa == null) return NotFound();
            return View(echipa);
        }

        [HttpPost, ActionName("Sterge")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeConfirmat(int id)
        {
            await _echipaService.StergeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}