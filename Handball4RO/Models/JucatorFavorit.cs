namespace Handball4RO.Models
{
    public class JucatorFavorit
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int JucatorId { get; set; }
        public Jucator Jucator { get; set; }
    }
}