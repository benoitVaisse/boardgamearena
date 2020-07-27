using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BoardGameArena.ViewModels
{
    public class FormRegister
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Pseudo")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Mot de passe")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage ="Les mots de passe douvent être identique")]
        public string ConfirmPassword { get; set; }
    }
}
