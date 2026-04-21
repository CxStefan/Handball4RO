using Microsoft.AspNetCore.Mvc;

public class MeciuriController : Controller
{
    private readonly IMeciService _meciService;

    public MeciuriController(IMeciService meciService) => _meciService = meciService;

    public async Task<IActionResult> Index(int idCompetitie)
    {
        var meciuri = await _meciService.ObtineMeciuriCompetitieAsync(idCompetitie);
        ViewBag.IdCompetitie = idCompetitie;
        return View(meciuri);
    }

    [HttpPost]
    public async Task<IActionResult> ActualizeazaScor(int id, int sg, int so, int idComp)
    {
        await _meciService.ActualizeazaScorAsync(id, sg, so);
        return RedirectToAction("Index", new { idCompetitie = idComp });
    }
}