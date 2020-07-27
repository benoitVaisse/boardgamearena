using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.ViewModels
{
    public class FormRoleAdminViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nom du Rôle")]
        [MaxLength(20)]
        public string Name { get; set; }

    }
}
