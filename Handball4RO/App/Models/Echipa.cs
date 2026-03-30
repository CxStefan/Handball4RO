using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Handball4RO.Models;

namespace Handball4RO.Models
{
    public class Echipa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nume { get; set; }

        public string LogoUrl { get; set; }
        public int? AnInfiintare { get; set; }
        public string Oras { get; set; }

        // o echipa are mai multi jucatori
        public ICollection<Jucator> Jucatori { get; set; }

        // o echipa poate juca fie acasa fie in deplasare
        [InverseProperty("EchipaGazda")]
        public ICollection<Meci> MeciuriAcasa { get; set; }

        [InverseProperty("EchipaOaspete")]
        public ICollection<Meci> MeciuriDeplasare { get; set; }

        public ICollection<Clasament> Clasamente { get; set; }
    }
}