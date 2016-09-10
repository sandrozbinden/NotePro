using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Models
{
    public class Todo : Controller
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        [NotMapped]
        public DateTime CreationDate { get; set; }

        public Todo()
        {
            CreationDate = DateTime.Now;
        }

    }
}
