using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotePro.Models.TodoViewModels;
using NotePro.Models;
using NotePro.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext context;

        public TodoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(NewTodoViewModel newTodo)
        {
            if (ModelState.IsValid)
            {
                var todo = new Todo() { Title = newTodo.Title, Text = newTodo.Text};
                context.Todos.Add(todo);
                context.SaveChanges();
                
                return RedirectToAction("Index","Home");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
