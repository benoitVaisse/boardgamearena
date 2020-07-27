using BoardGameArena.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.ViewModels
{
    public class FormGameViewModel
    {
        public int Id { get; set; }


        [Required(AllowEmptyStrings =false, ErrorMessage ="Le titre du jeu est obligatoire")]
        public string Title { get; set; }

        [Required]
        [Range(0,999)]
        [CompareNumeriqueValue("NbMax","<=", ErrorMessage ="Le nombre minimum de joueur doit être inferieur au nombre maximum")]
        public int NbMin { get; set; }

        [Required]
        [Range(0, 999)]
        public int NbMax { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Rules { get; set; }

        //[FileExtensions(Extensions ="jpg, jpeg, png")]
        [Required]
        public IFormFile CoverImage { get; set; }
    }
}
