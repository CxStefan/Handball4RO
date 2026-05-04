using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Handball4RO.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }



        public int? EchipaId { get; set; }

        [ForeignKey("EchipaId")]
        public virtual Echipa Echipa { get; set; }


        public int? JucatorId { get; set; }

        [ForeignKey("JucatorId")]
        public virtual Jucator Jucator { get; set; }


        public DateTime DataAdaugarii { get; set; } = DateTime.Now;
    }
}