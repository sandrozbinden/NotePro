using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotePro.Models.TodoViewModels;
using NotePro.Models;
using NotePro.Data;
using Microsoft.AspNetCore.Http;
using NotePro.ExtensionMethods;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
        }


        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Edit(long id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            var todoView = new TodoViewModel() { Id = todo.Id, Title = todo.Title, Text = todo.Text, FinishDate = todo.FinishDate, Priority = todo.Priority, Finished = todo.Finished };
            return View("Edit", todoView);
        }

        public IActionResult Update(long id, TodoViewModel todoView)
        {
            if (ModelState.IsValid)
            {
                var todo = new Todo() { Id = todoView.Id, Title = todoView.Title, Text = todoView.Text, FinishDate = todoView.FinishDate, Priority = todoView.Priority, Finished = todoView.Finished };
                _context.Todos.Update(todo);
                _context.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                return BadRequest();
            }
        }





        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(TodoViewModel newTodo)
        {
            if (ModelState.IsValid)
            {
                var todo = new Todo() { Title = newTodo.Title, Text = newTodo.Text, FinishDate= newTodo.FinishDate, Priority = newTodo.Priority, Finished = newTodo.Finished};
                _context.Todos.Add(todo);
                _context.SaveChanges();
                
                return RedirectToAction("List");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Sort(SortOrder sortOrder)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Todos.SortOrder", sortOrder.ToString());
            return PartialList();
        }

        

        public IActionResult ToggleLayout()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var defaultLayout = session.GetBoolean("Application.DefaultLayout", true);
            session.SetBoolean("Application.DefaultLayout", !defaultLayout);
            return List();
        }
    
        public IActionResult ToggleShowFinished()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var showFinished = session.GetBoolean("Todos.ShowFinished", true);
            session.SetBoolean("Todos.ShowFinished", !showFinished);
            return PartialList(); 
        }
        public IActionResult PartialList()
        {

            var session = _httpContextAccessor.HttpContext.Session;
            var sortOrder = session.GetString("Todos.SortOrder").ToEnum(SortOrder.FinishDate);
            var showFinished = session.GetBoolean("Todos.ShowFinished", true);
            return PartialView("ListContent", FindTodos(sortOrder, showFinished));
        }


        public IActionResult List()
        {
            
            var session = _httpContextAccessor.HttpContext.Session;
            var sortOrder = session.GetString("Todos.SortOrder").ToEnum(SortOrder.FinishDate);
            var showFinished = session.GetBoolean("Todos.ShowFinished", true);
            var todos = FindTodos(sortOrder, showFinished);
            return View("List", new TodoListViewModel { Todos = todos,SortOrder =sortOrder,  ShowFinished = showFinished});
        }


        private List<Todo> FindTodos(SortOrder sortOrder, bool showFinished)
        {
            switch (sortOrder)
            {
                case SortOrder.FinishDate: return _context.Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.FinishDate).ToList();
                case SortOrder.Priority: return _context.Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderByDescending(todo => todo.Priority).ToList();
                case SortOrder.CreatedDate: return _context.Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.CreationDate).ToList();
                default: throw new System.InvalidOperationException("Can't find sortOrder for enum: " + sortOrder);
            }
        }
    }
}
