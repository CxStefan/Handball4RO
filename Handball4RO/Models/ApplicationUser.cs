using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Handball4RO.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NumeComplet { get; set; }
        public string? PozaProfilUrl { get; set; }

        public List<EchipaFavorita> EchipeFavorite { get; set; } = new List<EchipaFavorita>();
        public List<JucatorFavorit> JucatoriFavoriti { get; set; } = new List<JucatorFavorit>();
    }
}