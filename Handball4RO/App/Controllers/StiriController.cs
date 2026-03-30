using Handball4RO.Data;
using Handball4RO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Handball4RO.Controllers
{
    public class StiriController : Controller
    {
        private readonly ApplicationDbContext _context;

        // injectam baza de date in controller
        public StiriController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET:  metoda pentru a afisa toate stirile din baza de date
        public async Task<IActionResult> Index()
        {
            // luam stirile din baza de date in ordine cronologica
            var stiri = await _context.Stiri
                                      .OrderByDescending(s => s.DataPublicare)
                                      .ToListAsync();

            // trimitem lista de stiri catre Index.cshtml
            return View(stiri);
        }


        // 1. afisam formularul gol
        public IActionResult Adauga()
        {
            return View();
        }

        // 2. POST: preia datele din formular si le salveaza in baza de date
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adauga(Stire stireNoua)
        {
            // deocamdata nu am logica de admin asa ca ignoram campul de admin
            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");
            // --------------------------------

            if (ModelState.IsValid)
            {
                stireNoua.DataPublicare = DateTime.Now; // Setăm data curentă
                                                        // Aici în viitor vei prelua AutorId din sesiunea adminului logat

                _context.Add(stireNoua);
                await _context.SaveChangesAsync(); // Salvează fizic în SQL

                return RedirectToAction(nameof(Index)); // Întoarce adminul la lista de știri
            }

            return View(stireNoua); // afisam formularul initial
        }



        // GET: deschidem pagina completata cu informatiile despre stiri
        public async Task<IActionResult> Editeaza(int? id)
        {
            if (id == null) return NotFound();

            var stire = await _context.Stiri.FindAsync(id);
            if (stire == null) return NotFound();

            return View(stire); // trimitem datele catre Editeaza.cshtml
        }

        // POST: salvam modificarile in baza de date
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(int id, Stire stireModificata)
        {
            if (id != stireModificata.Id) return NotFound();

            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");

            if (ModelState.IsValid)
            {
                _context.Update(stireModificata);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // ne intoarcem la pagina de stiri
            }
            return View(stireModificata);
        }



        // GET pentru pagina de confirmare
        public async Task<IActionResult> Sterge(int? id)
        {
            if (id == null) return NotFound();

            var stire = await _context.Stiri.FirstOrDefaultAsync(m => m.Id == id);
            if (stire == null) return NotFound();

            return View(stire); // trimitem catre sterge.cshtml (ptr confirmare)
        }

        // POST : metoda de stergere
        [HttpPost, ActionName("Sterge")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StergeConfirmare(int id)
        {
            var stire = await _context.Stiri.FindAsync(id);
            if (stire != null)
            {
                _context.Stiri.Remove(stire); // comanda de stergere
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}