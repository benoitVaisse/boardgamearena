using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.ViewModels
{
    public class FormLogin
    {

        public int Id { get; set; }

        public string Email { get; set; }

        [Display(Name ="Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
