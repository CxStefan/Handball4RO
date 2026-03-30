using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handball4RO.Models
{
    public class StatisticaJucator
    {
        [Key]
        public int Id { get; set; }

        // foreign keys
        public int JucatorId { get; set; }
        [ForeignKey("JucatorId")]
        public Jucator Jucator { get; set; }

        public int MeciId { get; set; }
        [ForeignKey("MeciId")]
        public Meci Meci { get; set; }

        // statisticile
        public int GoluriMarcate { get; set; } = 0;

        public int Assisturi { get; set; } = 0;

        public int Aruncari7mTransformate { get; set; } = 0;

        // pentru portari
        public int Parade { get; set; } = 0;

        // sanctiuni
        public int CartonaseGalbene { get; set; } = 0;
        public int Eliminari2Min { get; set; } = 0;
        public int CartonaseRosii { get; set; } = 0;
    }
}