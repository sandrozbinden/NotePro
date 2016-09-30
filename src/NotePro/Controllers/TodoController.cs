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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return View("Edit", todo);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(long id, Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Todos.Update(todo);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult ToggleLayout()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var defaultLayout = session.GetBoolean("Application.DefaultLayout", true);
            session.SetBoolean("Application.DefaultLayout", !defaultLayout);
            return Index();
        }

        [HttpPost]
        public IActionResult Sort(SortOrder sortOrder)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Todos.SortOrder", sortOrder.ToString());
            return PartialList();
        }

        [HttpPost]
        public IActionResult ToggleShowFinished()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var showFinished = session.GetBoolean("Todos.ShowFinished", true);
            session.SetBoolean("Todos.ShowFinished", !showFinished);
            return PartialList(); 
        }

        public IActionResult PartialList()
        {
            return PartialView("ListContent", _context.FindTodos(SortOrder(), ShowFinished()));
        }

        public IActionResult Index()
        {
            var sortOrder = SortOrder();
            var showFinished = ShowFinished();
            var todos = _context.FindTodos(sortOrder, showFinished);
            return View("Index", new TodoListViewModel { Todos = todos,SortOrder =sortOrder,  ShowFinished = showFinished});
        }

        private bool ShowFinished()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            return session.GetBoolean("Todos.ShowFinished", true);
        }

        public SortOrder SortOrder()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            return session.GetString("Todos.SortOrder").ToEnum(Models.TodoViewModels.SortOrder.FinishDate); ;
        }

    }
}
