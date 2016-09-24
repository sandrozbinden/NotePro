using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Models.TodoViewModels
{
    public class TodoViewModel
    {

        public long Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Required]
        [Range(1, 5)]
        public int Priority { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

    }

}
