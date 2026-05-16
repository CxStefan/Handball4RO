using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Handball4RO.Models
{
    public class Competitie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nume { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sezon { get; set; }

        
        public ICollection<Meci> Meciuri { get; set; }
        public ICollection<Clasament> Clasamente { get; set; }
    }
}