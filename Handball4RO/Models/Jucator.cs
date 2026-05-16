using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handball4RO.Models
{
    public class Jucator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nume { get; set; }

        [MaxLength(50)]
        public string Pozitie { get; set; }

        public int? NumarTricou { get; set; }

        
        public int? EchipaId { get; set; }

        [ForeignKey("EchipaId")]
        public Echipa Echipa { get; set; } 

        public ICollection<StatisticaJucator> Statistici { get; set; }
    }
}