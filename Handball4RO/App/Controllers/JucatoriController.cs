using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Services;

namespace Handball4RO.Controllers
{
    public class JucatoriController : Controller
    {
        private readonly IJucatorService _jucatorService;
        private readonly IEchipaService _echipaService;

        public JucatoriController(IJucatorService jucatorService, IEchipaService echipaService)
        {
            _jucatorService = jucatorService;
            _echipaService = echipaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int echipaId, int? competitieId)
        {
            var echipa = await _echipaService.ObtineDupaIdAsync(echipaId);
            if (echipa == null) return NotFound();

            ViewBag.NumeEchipa = echipa.Nume;
            ViewBag.EchipaId = echipaId;
            ViewBag.LogoEchipa = echipa.LogoUrl;

            ViewBag.CompetitieId = competitieId;

            var jucatori = await _jucatorService.ObtineJucatoriDupaEchipaAsync(echipaId);

            var jucatoriOrdonati = jucatori.OrderBy(j => j.NumarTricou ?? 999).ToList();

            return View(jucatoriOrdonati);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Jucator jucator, int? competitieId)
        {
            ModelState.Remove("Echipa");
            ModelState.Remove("Statistici");
            ModelState.Remove("StatisticiJucatori");

            if (ModelState.IsValid)
            {
                await _jucatorService.AdaugaAsync(jucator);
                TempData["Mesaj"] = "✅ Jucătorul a fost salvat cu succes!";
            }
            else
            {
                var erori = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                TempData["Eroare"] = "❌ Sistemul a blocat salvarea: " + erori;
            }

            return RedirectToAction(nameof(Index), new { echipaId = jucator.EchipaId, competitieId = competitieId });
        }


        [HttpGet]
        public async Task<IActionResult> Editeaza(int id, int? competitieId)
        {
            var jucator = await _jucatorService.ObtineDupaIdAsync(id);
            if (jucator == null) return NotFound();

            ViewBag.CompetitieId = competitieId;
            return View(jucator);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(Jucator jucator, int? competitieId) 
        {
            ModelState.Remove("Echipa");
            ModelState.Remove("Statistici");
            ModelState.Remove("StatisticiJucatori");

            if (ModelState.IsValid)
            {
                await _jucatorService.EditeazaAsync(jucator);
               
                return RedirectToAction(nameof(Index), new { echipaId = jucator.EchipaId, competitieId = competitieId });
            }

            ViewBag.CompetitieId = competitieId;
            return View(jucator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sterge(int id, int echipaId, int? competitieId)
        {
            await _jucatorService.StergeAsync(id);

            
            return RedirectToAction(nameof(Index), new { echipaId = echipaId, competitieId = competitieId });
        }
    }
}