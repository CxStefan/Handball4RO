using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handball4RO.Models
{
    public class Stire
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Titlu { get; set; }

        [Required]
        public string Continut { get; set; }

        public string ImagineUrl { get; set; }

        public DateTime DataPublicare { get; set; } = DateTime.Now;

        public string? AutorId { get; set; }

        [ForeignKey("AutorId")]
        public ApplicationUser Autor { get; set; }
    }
}