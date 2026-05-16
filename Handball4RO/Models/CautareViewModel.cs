namespace Handball4RO.Models
{
    public class CautareViewModel
    {
        public string TextCautat { get; set; }
        public List<Echipa> EchipeGasite { get; set; } = new List<Echipa>();
        public List<Jucator> JucatoriGasiti { get; set; } = new List<Jucator>();
    }
}