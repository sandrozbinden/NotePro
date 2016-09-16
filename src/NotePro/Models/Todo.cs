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

        public string Title { get; set; }

        public string Text { get; set; }
 
        public int Priority { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime FinishDate { get; set; }

        public bool Finished { get; set; }

        public Todo()
        {
            CreationDate = DateTime.Now;
        }

    }
}
