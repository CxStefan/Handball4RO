using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Handball4RO.Models;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    public class ClasamentController : Controller
    {
        private readonly IClasamentService _clasamentService;
        private readonly IEchipaService _echipaService;

        public ClasamentController(IClasamentService clasamentService, IEchipaService echipaService)
        {
            _clasamentService = clasamentService;
            _echipaService = echipaService;
        }


        [HttpGet]
        public async Task<IActionResult> AdaugaInCompetitie(int competitieId)
        {
            var echipe = await _echipaService.ObtineToateAsync();
            ViewBag.Echipe = new SelectList(echipe, "Id", "Nume");

            return View(new Clasament { CompetitieId = competitieId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdaugaInCompetitie(Clasament clasament)
        {
            ModelState.Remove("Echipa");
            ModelState.Remove("Competitie");

            if (ModelState.IsValid)
            {
                await _clasamentService.AdaugaInClasamentAsync(clasament);

                return RedirectToAction("Details", "Competitii", new { id = clasament.CompetitieId });
            }

            var echipe = await _echipaService.ObtineToateAsync();
            ViewBag.Echipe = new SelectList(echipe, "Id", "Nume", clasament.EchipaId);

            return View(clasament);
        }


        [HttpGet]
        public async Task<IActionResult> Editeaza(int id)
        {
            var item = await _clasamentService.ObtineDupaIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(Clasament model)
        {
            ModelState.Remove("Echipa");
            ModelState.Remove("Competitie");

            if (ModelState.IsValid)
            {
                await _clasamentService.ActualizeazaDateAsync(model);
                return RedirectToAction("Details", "Competitii", new { id = model.CompetitieId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Sterge(int id)
        {
            var item = await _clasamentService.ObtineDupaIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Sterge")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeConfirmare(int id, int competitieId)
        {
            await _clasamentService.StergeDinClasamentAsync(id);
            return RedirectToAction("Details", "Competitii", new { id = competitieId });
        }
    }
}