using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Controllers
{
    public class CautareController : Controller
    {
        private readonly IGenericRepository<Echipa> _echipaRepo;
        private readonly IGenericRepository<Jucator> _jucatorRepo;

        public CautareController(IGenericRepository<Echipa> echipaRepo, IGenericRepository<Jucator> jucatorRepo)
        {
            _echipaRepo = echipaRepo;
            _jucatorRepo = jucatorRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string query)
        {
            var model = new CautareViewModel { TextCautat = query };

            if (!string.IsNullOrWhiteSpace(query))
            {
                var search = query.ToLower();

                // cautam echipe
                var toateEchipele = await _echipaRepo.GetAllAsync();
                model.EchipeGasite = toateEchipele
                    .Where(e => e.Nume.ToLower().Contains(search))
                    .ToList();

                // cautam jucatori
                var totiJucatorii = await _jucatorRepo.GetAllAsync();
                model.JucatoriGasiti = totiJucatorii
                    .Where(j => j.Nume.ToLower().Contains(search))
                    .ToList();
            }

            return View(model);
        }
    }
}