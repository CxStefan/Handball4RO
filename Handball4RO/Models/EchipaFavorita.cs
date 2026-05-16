namespace Handball4RO.Models
{
    public class EchipaFavorita
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int EchipaId { get; set; }
        public Echipa Echipa { get; set; }
    }
}