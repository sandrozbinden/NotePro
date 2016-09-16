using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotePro.Models.TodoViewModels;
using NotePro.Models;
using NotePro.Data;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext context;
        private IHttpContextAccessor httpContextAccessor;

        public TodoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
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

        public IActionResult Edit(long id)
        {
            var todo = context.Todos.Where(x => x.Id == id).FirstOrDefault();
            if (todo == null)
            {
                return NotFound();
            }
            var todoView = new NewTodoViewModel() { Id = todo.Id, Title = todo.Title, Text = todo.Text, FinishDate = todo.FinishDate, Priority = todo.Priority, Finished = todo.Finished };
            return View("Edit", todoView);
        }

        public IActionResult Update(long id, NewTodoViewModel todoView)
        {
            var todo = new Todo() { Id = todoView.Id, Title = todoView.Title, Text = todoView.Text, FinishDate = todoView.FinishDate, Priority = todoView.Priority, Finished = todoView.Finished };
            context.Todos.Update(todo);
            context.SaveChanges();

            return RedirectToAction("List");
        }





        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(NewTodoViewModel newTodo)
        {
            if (ModelState.IsValid)
            {
                var todo = new Todo() { Title = newTodo.Title, Text = newTodo.Text, FinishDate= newTodo.FinishDate, Priority = newTodo.Priority, Finished = newTodo.Finished};
                context.Todos.Add(todo);
                context.SaveChanges();
                
                return RedirectToAction("List");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult List(string sortOrder, bool showFinished, string layoutStyle)
        {
            if (!String.IsNullOrEmpty(layoutStyle))
            {
                httpContextAccessor.HttpContext.Session.SetString("Application.LayoutStyle", layoutStyle);
            }
            switch (sortOrder)
            {
                case "finishDate": return View("List", context.Todos.Where(todo => todo.Finished == false || todo.Finished ==  showFinished).OrderBy(todo => todo.FinishDate).ToList());
                case "priority": return View("List", context.Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderByDescending(todo => todo.Priority).ToList());
                case "createdDate": 
                default: return View("List", context.Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.CreationDate).ToList());
            }
            
        }

    }
}
