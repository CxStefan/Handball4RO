using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Handball4RO.Models;

namespace Handball4RO.Models
{
    public class Meci
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataMeci { get; set; }

        public int? ScorGazda { get; set; }
        public int? ScorOaspete { get; set; }

        public string Status { get; set; } = "urmeaza"; // urmeaza, in_desfasurare, finalizat

        // legatura cu competitie
        public int CompetitieId { get; set; }
        [ForeignKey("CompetitieId")]
        public Competitie Competitie { get; set; }

        // legatura cu echipa gazda
        public int EchipaGazdaId { get; set; }
        [ForeignKey("EchipaGazdaId")]
        public Echipa EchipaGazda { get; set; }

        // legatura cu echipa oaspete
        public int EchipaOaspeteId { get; set; }
        [ForeignKey("EchipaOaspeteId")]
        public Echipa EchipaOaspete { get; set; }

        public ICollection<StatisticaJucator> StatisticiJucatori { get; set; }
    }
}