using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Handball4RO.Models
{
    public class StatisticiCompetitieViewModel
    {
        public int? CompetitieId { get; set; }

        public SelectList ListaCompetitii { get; set; }

        public List<JucatorAgregat> TopMarcatori { get; set; } = new List<JucatorAgregat>();
        public List<JucatorAgregat> TopPortari { get; set; } = new List<JucatorAgregat>();
    }

    public class JucatorAgregat
    {
        public string NumeJucator { get; set; }
        public string NumeEchipa { get; set; }
        public string Pozitie { get; set; }

        public int MeciuriJucate { get; set; }
        public int TotalGoluri { get; set; }
        public int Total7m { get; set; }
        public int TotalAssisturi { get; set; }
        public int TotalParade { get; set; }
        public int TotalGalbene { get; set; }
        public int Total2Min { get; set; }
        public int TotalRosii { get; set; }
    }
}