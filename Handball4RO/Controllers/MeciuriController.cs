using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Handball4RO.Models;
using Handball4RO.Services;
using Handball4RO.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Handball4RO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MeciuriController : Controller
    {
        private readonly IMeciService _meciService;
        private readonly IEchipaService _echipaService;
        private readonly IJucatorService _jucatorService;

        private readonly IGenericRepository<StatisticaJucator> _statisticaRepo;
        private readonly IGenericRepository<Clasament> _clasamentRepo;

        public MeciuriController(
            IMeciService meciService,
            IEchipaService echipaService,
            IJucatorService jucatorService,
            IGenericRepository<StatisticaJucator> statisticaRepo,
            IGenericRepository<Clasament> clasamentRepo)
        {
            _meciService = meciService;
            _echipaService = echipaService;
            _jucatorService = jucatorService;
            _statisticaRepo = statisticaRepo;
            _clasamentRepo = clasamentRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int competitieId)
        {
            ViewBag.CompetitieId = competitieId;

            var echipe = await _echipaService.ObtineEchipeDupaCompetitieAsync(competitieId);
            ViewBag.EchipeMap = echipe.ToDictionary(e => e.Id, e => e.Nume);

            var meciuri = await _meciService.ObtineMeciuriDupaCompetitieAsync(competitieId);
            return View(meciuri);
        }

        [HttpGet]
        public async Task<IActionResult> Adauga(int competitieId)
        {
            ViewBag.CompetitieId = competitieId;
            var echipeDinLiga = await _echipaService.ObtineEchipeDupaCompetitieAsync(competitieId);
            ViewBag.ListaEchipe = new SelectList(echipeDinLiga, "Id", "Nume");

            return View(new Meci
            {
                CompetitieId = competitieId,
                DataMeci = DateTime.Now
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Meci meci)
        {
            ModelState.Remove("Competitie");
            ModelState.Remove("EchipaGazda");
            ModelState.Remove("EchipaOaspete");
            ModelState.Remove("StatisticiJucatori");

            if (meci.EchipaGazdaId == meci.EchipaOaspeteId)
            {
                ModelState.AddModelError("", "Echipa gazdă nu poate fi aceeași cu echipa oaspete!");
            }

            if (ModelState.IsValid)
            {
                await _meciService.AdaugaAsync(meci);
                return RedirectToAction(nameof(Index), new { competitieId = meci.CompetitieId });
            }

            var echipeDinLiga = await _echipaService.ObtineEchipeDupaCompetitieAsync(meci.CompetitieId);
            ViewBag.ListaEchipe = new SelectList(echipeDinLiga, "Id", "Nume");
            ViewBag.CompetitieId = meci.CompetitieId;

            return View(meci);
        }

        [HttpGet]
        public async Task<IActionResult> Editeaza(int id, int? competitieId)
        {
            var meci = await _meciService.ObtineDupaIdAsync(id);
            if (meci == null) return NotFound();

            ViewBag.CompetitieId = competitieId;
            var echipeDinLiga = await _echipaService.ObtineEchipeDupaCompetitieAsync(competitieId ?? meci.CompetitieId);
            ViewBag.ListaEchipe = new SelectList(echipeDinLiga, "Id", "Nume");

            return View(meci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(Meci meci, int? competitieId)
        {
            ModelState.Remove("Competitie");
            ModelState.Remove("EchipaGazda");
            ModelState.Remove("EchipaOaspete");
            ModelState.Remove("StatisticiJucatori");

            if (meci.EchipaGazdaId == meci.EchipaOaspeteId)
            {
                ModelState.AddModelError("", "Echipa gazdă nu poate fi aceeași cu echipa oaspete!");
            }

            if (ModelState.IsValid)
            {
                await _meciService.EditeazaAsync(meci);
                return RedirectToAction(nameof(Index), new { competitieId = competitieId });
            }

            ViewBag.CompetitieId = competitieId;
            var echipeDinLiga = await _echipaService.ObtineEchipeDupaCompetitieAsync(competitieId ?? meci.CompetitieId);
            ViewBag.ListaEchipe = new SelectList(echipeDinLiga, "Id", "Nume");
            return View(meci);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sterge(int id, int? competitieId)
        {
            var meci = await _meciService.ObtineDupaIdAsync(id);
            if (meci != null)
            {
                if (meci.Status == "finalizat")
                {
                   
                    var toateStatisticile = await _statisticaRepo.GetAllAsync();
                    var statisticiMeci = toateStatisticile.Where(s => s.MeciId == id).ToList();
                    foreach (var stat in statisticiMeci)
                    {
                        await _statisticaRepo.DeleteAsync(stat);
                    }

                   
                    var toateClasamentele = await _clasamentRepo.GetAllAsync();
                    var clasamentGazda = toateClasamentele.FirstOrDefault(c => c.EchipaId == meci.EchipaGazdaId && c.CompetitieId == (competitieId ?? meci.CompetitieId));
                    var clasamentOaspete = toateClasamentele.FirstOrDefault(c => c.EchipaId == meci.EchipaOaspeteId && c.CompetitieId == (competitieId ?? meci.CompetitieId));

                    if (clasamentGazda != null && clasamentOaspete != null)
                    {
                        
                        clasamentGazda.MeciuriJucate--;
                        clasamentOaspete.MeciuriJucate--;

                        
                        clasamentGazda.GoluriMarcate -= meci.ScorGazda ?? 0;
                        clasamentGazda.GoluriPrimite -= meci.ScorOaspete ?? 0;
                        clasamentOaspete.GoluriMarcate -= meci.ScorOaspete ?? 0;
                        clasamentOaspete.GoluriPrimite -= meci.ScorGazda ?? 0;

                        
                        if (meci.ScorGazda > meci.ScorOaspete)
                        {
                            clasamentGazda.Victorii--;
                            clasamentGazda.Puncte -= 3;
                            clasamentOaspete.Infrangeri--;
                        }
                        else if (meci.ScorGazda < meci.ScorOaspete)
                        {
                            clasamentOaspete.Victorii--;
                            clasamentOaspete.Puncte -= 3;
                            clasamentGazda.Infrangeri--;
                        }
                        else
                        {
                            clasamentGazda.Egaluri--;
                            clasamentOaspete.Egaluri--;
                            clasamentGazda.Puncte -= 1;
                            clasamentOaspete.Puncte -= 1;
                        }

                        
                        await _clasamentRepo.UpdateAsync(clasamentGazda);
                        await _clasamentRepo.UpdateAsync(clasamentOaspete);
                    }
                }

                
                await _meciService.StergeAsync(id);
            }

            return RedirectToAction(nameof(Index), new { competitieId = competitieId });
        }

        [HttpGet]
        public async Task<IActionResult> Finalizeaza(int id)
        {
            var meci = await _meciService.ObtineDupaIdAsync(id);
            if (meci == null) return NotFound();

            var gazda = await _echipaService.ObtineDupaIdAsync(meci.EchipaGazdaId);
            var oaspete = await _echipaService.ObtineDupaIdAsync(meci.EchipaOaspeteId);

            var jucatoriGazda = await _jucatorService.ObtineJucatoriDupaEchipaAsync(gazda.Id);
            var jucatoriOaspete = await _jucatorService.ObtineJucatoriDupaEchipaAsync(oaspete.Id);

            var viewModel = new FinalizareMeciViewModel
            {
                MeciId = meci.Id,
                CompetitieId = meci.CompetitieId,
                NumeGazda = gazda.Nume,
                NumeOaspete = oaspete.Nume,
                JucatoriGazda = jucatoriGazda.Select(j => new JucatorStatInfo { JucatorId = j.Id, Nume = j.Nume, Pozitie = j.Pozitie }).ToList(),
                JucatoriOaspete = jucatoriOaspete.Select(j => new JucatorStatInfo { JucatorId = j.Id, Nume = j.Nume, Pozitie = j.Pozitie }).ToList()
            };

            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalizeaza(FinalizareMeciViewModel model)
        {
            
            var meci = await _meciService.ObtineDupaIdAsync(model.MeciId);
            meci.ScorGazda = model.ScorGazda;
            meci.ScorOaspete = model.ScorOaspete;
            meci.Status = "finalizat";
            await _meciService.EditeazaAsync(meci);

          
            var totiJucatorii = model.JucatoriGazda.Concat(model.JucatoriOaspete);
            foreach (var j in totiJucatorii)
            {
                if (j.GoluriMarcate > 0 || j.Assisturi > 0 || j.Aruncari7mTransformate > 0 ||
                    j.Parade > 0 || j.CartonaseGalbene > 0 || j.Eliminari2Min > 0 || j.CartonaseRosii > 0)
                {
                    var stat = new StatisticaJucator
                    {
                        MeciId = meci.Id,
                        JucatorId = j.JucatorId,
                        GoluriMarcate = j.GoluriMarcate,
                        Assisturi = j.Assisturi,
                        Aruncari7mTransformate = j.Aruncari7mTransformate,
                        Parade = j.Parade,
                        CartonaseGalbene = j.CartonaseGalbene,
                        Eliminari2Min = j.Eliminari2Min,
                        CartonaseRosii = j.CartonaseRosii
                    };

                    await _statisticaRepo.AddAsync(stat);
                }
            }

            
            var toateClasamentele = await _clasamentRepo.GetAllAsync();

            var clasamentGazda = toateClasamentele.FirstOrDefault(c => c.EchipaId == meci.EchipaGazdaId && c.CompetitieId == model.CompetitieId);
            var clasamentOaspete = toateClasamentele.FirstOrDefault(c => c.EchipaId == meci.EchipaOaspeteId && c.CompetitieId == model.CompetitieId);

            if (clasamentGazda != null && clasamentOaspete != null)
            {
                clasamentGazda.MeciuriJucate++;
                clasamentOaspete.MeciuriJucate++;

                clasamentGazda.GoluriMarcate += model.ScorGazda;
                clasamentGazda.GoluriPrimite += model.ScorOaspete;

                clasamentOaspete.GoluriMarcate += model.ScorOaspete;
                clasamentOaspete.GoluriPrimite += model.ScorGazda;

                if (model.ScorGazda > model.ScorOaspete)
                {
                    clasamentGazda.Victorii++;
                    clasamentGazda.Puncte += 3;
                    clasamentOaspete.Infrangeri++;
                }
                else if (model.ScorGazda < model.ScorOaspete)
                {
                    clasamentOaspete.Victorii++;
                    clasamentOaspete.Puncte += 3;
                    clasamentGazda.Infrangeri++;
                }
                else
                {
                    clasamentGazda.Egaluri++;
                    clasamentOaspete.Egaluri++;
                    clasamentGazda.Puncte += 1;
                    clasamentOaspete.Puncte += 1;
                }

                await _clasamentRepo.UpdateAsync(clasamentGazda);
                await _clasamentRepo.UpdateAsync(clasamentOaspete);
            }

            return RedirectToAction(nameof(Index), new { competitieId = model.CompetitieId });
        }
    }
}