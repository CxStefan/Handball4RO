using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Controllers
{
    public class AstaziController : Controller
    {
        private readonly IGenericRepository<Meci> _meciRepo;
        private readonly IGenericRepository<Echipa> _echipaRepo;
        private readonly IGenericRepository<Competitie> _competitieRepo;

        public AstaziController(
            IGenericRepository<Meci> meciRepo,
            IGenericRepository<Echipa> echipaRepo,
            IGenericRepository<Competitie> competitieRepo)
        {
            _meciRepo = meciRepo;
            _echipaRepo = echipaRepo;
            _competitieRepo = competitieRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var azi = DateTime.Today;

            var toateMeciurile = await _meciRepo.GetAllAsync();
            var meciuriAzi = toateMeciurile
                .Where(m => m.DataMeci.Date == azi)
                .OrderBy(m => m.DataMeci)
                .ToList();

            var echipe = await _echipaRepo.GetAllAsync();
            ViewBag.EchipeMap = echipe.ToDictionary(e => e.Id, e => e.Nume);

            var competitii = await _competitieRepo.GetAllAsync();
            ViewBag.CompetitiiMap = competitii.ToDictionary(c => c.Id, c => c.Nume);

            return View(meciuriAzi);
        }
    }
}