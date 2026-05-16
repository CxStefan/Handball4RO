using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // 1. Adăugăm librăria pentru Securitate
using Handball4RO.Models;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompetitiiController : Controller
    {
        private readonly ICompetitieService _competitieService;

        public CompetitiiController(ICompetitieService competitieService)
        {
            _competitieService = competitieService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var competitii = await _competitieService.ObtineToateCompetitiileAsync();
            return View(competitii);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var competitie = await _competitieService.ObtineCompetitieDupaIdAsync(id);
            if (competitie == null) return NotFound();

            ViewBag.NumeCompetitie = competitie.Nume;
            ViewBag.Sezon = competitie.Sezon;

            var clasament = await _competitieService.ObtineClasamentAsync(id);

            return View(clasament);
        }


        public IActionResult Adauga() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Competitie competitie)
        {
            ModelState.Remove("Meciuri");
            ModelState.Remove("Clasamente");

            if (ModelState.IsValid)
            {
                await _competitieService.AdaugaCompetitieAsync(competitie);
                return RedirectToAction(nameof(Index));
            }
            return View(competitie);
        }

        public async Task<IActionResult> Editeaza(int? id)
        {
            if (id == null) return NotFound();

            var competitie = await _competitieService.ObtineCompetitieDupaIdAsync(id.Value);
            if (competitie == null) return NotFound();

            return View(competitie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(int id, Competitie competitie)
        {
            if (id != competitie.Id) return NotFound();

            ModelState.Remove("Meciuri");
            ModelState.Remove("Clasamente");

            if (ModelState.IsValid)
            {
                await _competitieService.EditeazaCompetitieAsync(competitie);
                return RedirectToAction(nameof(Index));
            }
            return View(competitie);
        }

        public async Task<IActionResult> Sterge(int? id)
        {
            if (id == null) return NotFound();

            var competitie = await _competitieService.ObtineCompetitieDupaIdAsync(id.Value);
            if (competitie == null) return NotFound();

            return View(competitie);
        }

        [HttpPost, ActionName("Sterge")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeConfirmare(int id)
        {
            await _competitieService.StergeCompetitieAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}