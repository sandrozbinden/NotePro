using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Models
{
    public class Todo 
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

        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

        public Todo()
        {
            CreationDate = DateTime.Now;
        }

    }
}
