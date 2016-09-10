using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Models.TodoViewModels
{
    public class NewTodoViewModel
    {
        [Display(Name = "Title")]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Text")]
        [StringLength(1000, MinimumLength = 1)] //TODO change to unlimmited
        public string Text { get; set; }

    }

}
