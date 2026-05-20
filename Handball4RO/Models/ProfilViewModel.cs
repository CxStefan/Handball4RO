using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Handball4RO.Models
{
    public class ProfilViewModel
    {
        public string? NumeComplet { get; set; }

        public string? PozaProfilUrl { get; set; }

        [Display(Name = "Încarcă o poză de profil")]
        public IFormFile? FisierPoza { get; set; }
    }
}