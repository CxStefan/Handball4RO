using Microsoft.AspNetCore.Mvc;
using Handball4RO.Models;
using Handball4RO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace Handball4RO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StiriController : Controller
    {
        private readonly IStireService _stireService;
        private readonly IWebHostEnvironment _env; 

        public StiriController(IStireService stireService, IWebHostEnvironment env)
        {
            _stireService = stireService;
            _env = env;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var stiri = await _stireService.ObtineToateStirileAsync();
            return View(stiri.OrderByDescending(s => s.DataPublicare));
        }

        [AllowAnonymous]
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
        public async Task<IActionResult> Adauga(Stire stireNoua, IFormFile? fisierPoza)
        {
            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");
            ModelState.Remove("ImagineUrl"); 

            if (ModelState.IsValid)
            {
                if (fisierPoza != null && fisierPoza.Length > 0)
                {
                    string folderStiri = Path.Combine(_env.WebRootPath, "images", "stiri");
                    if (!Directory.Exists(folderStiri)) Directory.CreateDirectory(folderStiri);

                    string numeFisier = Guid.NewGuid().ToString() + Path.GetExtension(fisierPoza.FileName);
                    string caleaCompleta = Path.Combine(folderStiri, numeFisier);

                    using (var stream = new FileStream(caleaCompleta, FileMode.Create))
                    {
                        await fisierPoza.CopyToAsync(stream);
                    }

                    stireNoua.ImagineUrl = "/images/stiri/" + numeFisier;
                }

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
        public async Task<IActionResult> Editeaza(int id, Stire stireModificata, IFormFile? fisierPoza)
        {
            if (id != stireModificata.Id) return NotFound();
            ModelState.Remove("Autor");
            ModelState.Remove("AutorId");
            ModelState.Remove("ImagineUrl"); 

            if (ModelState.IsValid)
            {
                if (fisierPoza != null && fisierPoza.Length > 0)
                {
                    string folderStiri = Path.Combine(_env.WebRootPath, "images", "stiri");
                    if (!Directory.Exists(folderStiri)) Directory.CreateDirectory(folderStiri);

                    string numeFisier = Guid.NewGuid().ToString() + Path.GetExtension(fisierPoza.FileName);
                    string caleaCompleta = Path.Combine(folderStiri, numeFisier);

                    using (var stream = new FileStream(caleaCompleta, FileMode.Create))
                    {
                        await fisierPoza.CopyToAsync(stream);
                    }

                    stireModificata.ImagineUrl = "/images/stiri/" + numeFisier;
                }

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