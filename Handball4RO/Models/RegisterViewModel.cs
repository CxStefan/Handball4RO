using System.ComponentModel.DataAnnotations;

namespace Handball4RO.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Adresa de email nu este validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie.")]
        [MinLength(6, ErrorMessage = "Parola trebuie să aibă minim 6 caractere.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmarea parolei este obligatorie.")]
        [Compare("Password", ErrorMessage = "Parolele nu coincid!")]
        public string ConfirmPassword { get; set; }
    }
}