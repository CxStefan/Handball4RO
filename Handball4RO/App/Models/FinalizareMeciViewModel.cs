using System.Collections.Generic;

namespace Handball4RO.Models
{
    public class FinalizareMeciViewModel
    {
        public int MeciId { get; set; }
        public int CompetitieId { get; set; }

        public string NumeGazda { get; set; }
        public string NumeOaspete { get; set; }

        public int ScorGazda { get; set; }
        public int ScorOaspete { get; set; }

        public List<JucatorStatInfo> JucatoriGazda { get; set; } = new List<JucatorStatInfo>();
        public List<JucatorStatInfo> JucatoriOaspete { get; set; } = new List<JucatorStatInfo>();
    }

    public class JucatorStatInfo
    {
        public int JucatorId { get; set; }
        public string Nume { get; set; }
        public string Pozitie { get; set; }

        public int GoluriMarcate { get; set; }
        public int Assisturi { get; set; }
        public int Aruncari7mTransformate { get; set; }
        public int Parade { get; set; }
        public int CartonaseGalbene { get; set; }
        public int Eliminari2Min { get; set; }
        public int CartonaseRosii { get; set; }
    }
}