using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    public class StiriController : Controller
    {
        private readonly IStireService _stireService;

        public StiriController(IStireService stireService)
        {
            _stireService = stireService;
        }

        public async Task<IActionResult> Index()
        {
            var stiri = await _stireService.ObtineToateStirileAsync();
            return View(stiri.OrderByDescending(s => s.DataPublicare));
        }

        public async Task<IActionResult> Detalii(int? id)
        {
            if (id == null) return NotFound();
            var stire = await _stireService.ObtineStireDupaIdAsync(id.Value);
            if (stire == null) return NotFound();
            return View(stire);
        }

        public IActionResult Adauga() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Stire stireNoua)
        {
            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");

            if (ModelState.IsValid)
            {
                await _stireService.AdaugaStireAsync(stireNoua);
                return RedirectToAction(nameof(Index));
            }
            return View(stireNoua);
        }


        public async Task<IActionResult> Editeaza(int? id)
        {
            if (id == null) return NotFound();
            var stire = await _stireService.ObtineStireDupaIdAsync(id.Value);
            if (stire == null) return NotFound();
            return View(stire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(int id, Stire stireModificata)
        {
            if (id != stireModificata.Id) return NotFound();
            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");

            if (ModelState.IsValid)
            {
                await _stireService.EditeazaStireAsync(stireModificata);
                return RedirectToAction(nameof(Index));
            }
            return View(stireModificata);
        }

        public async Task<IActionResult> Sterge(int? id)
        {
            if (id == null) return NotFound();
            var stire = await _stireService.ObtineStireDupaIdAsync(id.Value);
            if (stire == null) return NotFound();
            return View(stire);
        }

        [HttpPost, ActionName("Sterge")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeConfirmare(int id)
        {
            await _stireService.StergeStireAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}