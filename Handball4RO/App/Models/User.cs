using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Handball4RO.Models;

namespace Handball4RO.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nume { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Parola { get; set; }

        [MaxLength(20)]
        public string Rol { get; set; } = "user";

        public DateTime DataInregistrare { get; set; } = DateTime.Now;

        // un user poate scrie mai multe stiri
        public ICollection<Stire> Stiri { get; set; }
    }
}