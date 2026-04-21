using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    public class EchipeController : Controller
    {
        private readonly IEchipaService _echipaService;

        public EchipeController(IEchipaService echipaService)
        {
            _echipaService = echipaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _echipaService.ObtineToateAsync());
        }

        public IActionResult Adauga() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Echipa echipa)
        {
            ModelState.Remove("Jucatori");
            ModelState.Remove("MeciuriAcasa");
            ModelState.Remove("MeciuriDeplasare");
            ModelState.Remove("Clasamente");

            if (ModelState.IsValid)
            {
                await _echipaService.AdaugaAsync(echipa);
                return RedirectToAction(nameof(Index));
            }
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