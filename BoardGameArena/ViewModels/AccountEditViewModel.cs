using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.ViewModels
{
    public class AccountEditViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public IFormFile Photo { get; set; }
    }
}
