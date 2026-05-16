using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Handball4RO.Models;

namespace Handball4RO.Models
{
    public class Clasament
    {
        [Key]
        public int Id { get; set; }

        public int MeciuriJucate { get; set; } = 0;
        public int Victorii { get; set; } = 0;
        public int Egaluri { get; set; } = 0;
        public int Infrangeri { get; set; } = 0;
        public int GoluriMarcate { get; set; } = 0;
        public int GoluriPrimite { get; set; } = 0;
        public int Puncte { get; set; } = 0;

        public int CompetitieId { get; set; }
        [ForeignKey("CompetitieId")]
        public Competitie Competitie { get; set; }

        public int EchipaId { get; set; }
        [ForeignKey("EchipaId")]
        public Echipa Echipa { get; set; }
    }
}