using Handball4RO.Models;

namespace Handball4RO.Models
{
    public class Echipa
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Oras { get; set; }
        public string LogoUrl { get; set; }

        public virtual ICollection<Jucator> Jucatori { get; set; } = new List<Jucator>();
        public virtual ICollection<Meci> MeciuriAcasa { get; set; } = new List<Meci>();
        public virtual ICollection<Meci> MeciuriDeplasare { get; set; } = new List<Meci>();
        public virtual ICollection<Clasament> Clasamente { get; set; } = new List<Clasament>();
    }
}