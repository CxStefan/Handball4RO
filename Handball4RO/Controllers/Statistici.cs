using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Handball4RO.Models;
using Handball4RO.Repositories;

namespace Handball4RO.Controllers
{
    public class StatisticiController : Controller
    {
        private readonly IGenericRepository<Competitie> _competitieRepo;
        private readonly IGenericRepository<Meci> _meciRepo;
        private readonly IGenericRepository<StatisticaJucator> _statisticaRepo;
        private readonly IGenericRepository<Jucator> _jucatorRepo;
        private readonly IGenericRepository<Echipa> _echipaRepo;

        public StatisticiController(
            IGenericRepository<Competitie> competitieRepo,
            IGenericRepository<Meci> meciRepo,
            IGenericRepository<StatisticaJucator> statisticaRepo,
            IGenericRepository<Jucator> jucatorRepo,
            IGenericRepository<Echipa> echipaRepo)
        {
            _competitieRepo = competitieRepo;
            _meciRepo = meciRepo;
            _statisticaRepo = statisticaRepo;
            _jucatorRepo = jucatorRepo;
            _echipaRepo = echipaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? competitieId)
        {
            var competitii = await _competitieRepo.GetAllAsync();
            var model = new StatisticiCompetitieViewModel
            {
                ListaCompetitii = new SelectList(competitii, "Id", "Nume", competitieId),
                CompetitieId = competitieId
            };

            if (competitieId.HasValue)
            {
                var meciuriLiga = (await _meciRepo.GetAllAsync()).Where(m => m.CompetitieId == competitieId.Value).Select(m => m.Id).ToList();
                var toateStatisticile = await _statisticaRepo.GetAllAsync();
                var totiJucatorii = await _jucatorRepo.GetAllAsync();
                var toateEchipele = await _echipaRepo.GetAllAsync();

                var statisticiFiltrare = toateStatisticile.Where(s => meciuriLiga.Contains(s.MeciId)).ToList();

                var grupuri = statisticiFiltrare.GroupBy(s => s.JucatorId);
                var listaAgregata = new List<JucatorAgregat>();

                foreach (var grup in grupuri)
                {
                    var jucator = totiJucatorii.FirstOrDefault(j => j.Id == grup.Key);
                    if (jucator != null)
                    {
                        var echipa = toateEchipele.FirstOrDefault(e => e.Id == jucator.EchipaId);

                        listaAgregata.Add(new JucatorAgregat
                        {
                            NumeJucator = jucator.Nume,
                            NumeEchipa = echipa?.Nume ?? "Fără Echipă",
                            Pozitie = jucator.Pozitie,

                            MeciuriJucate = grup.Count(), 
                            TotalGoluri = grup.Sum(s => s.GoluriMarcate),
                            Total7m = grup.Sum(s => s.Aruncari7mTransformate),
                            TotalAssisturi = grup.Sum(s => s.Assisturi),
                            TotalParade = grup.Sum(s => s.Parade),
                            TotalGalbene = grup.Sum(s => s.CartonaseGalbene),
                            Total2Min = grup.Sum(s => s.Eliminari2Min),
                            TotalRosii = grup.Sum(s => s.CartonaseRosii)
                        });
                    }
                }

                model.TopMarcatori = listaAgregata
                    .Where(j => j.Pozitie != "Portar")
                    .OrderByDescending(j => j.TotalGoluri) 
                    .ToList();

                model.TopPortari = listaAgregata
                    .Where(j => j.Pozitie == "Portar")
                    .OrderByDescending(j => j.TotalParade) 
                    .ToList();
            }

            return View(model);
        }
    }
}