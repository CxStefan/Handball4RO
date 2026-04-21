using Microsoft.AspNetCore.Mvc;

namespace Handball4RO.Controllers
{
    public class ContactController : Controller
    {
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string Nume, string Email, string Subiect, string Mesaj)
        {
            TempData["MesajSucces"] = $"Mulțumim, {Nume}! Mesajul tău despre '{Subiect}' a fost înregistrat.";

            return RedirectToAction(nameof(Index));
        }
    }
}