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
        private readonly TodoSessionAccessor _todoSessionAccessor;

        public TodoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this._context = context;
            this._todoSessionAccessor = new TodoSessionAccessor(httpContextAccessor);
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
            _todoSessionAccessor.DefaultLayout = !_todoSessionAccessor.DefaultLayout;
            return Index();
        }

        [HttpPost]
        public IActionResult Sort(SortOrder sortOrder)
        {
            _todoSessionAccessor.SortOrder = sortOrder;
            return PartialList();
        }

        [HttpPost]
        public IActionResult ToggleShowFinished()
        {
            _todoSessionAccessor.ShowFinished = !_todoSessionAccessor.ShowFinished;
            return PartialList(); 
        }

        public IActionResult PartialList()
        {
            return PartialView("ListContent", _context.FindTodos(_todoSessionAccessor.SortOrder, _todoSessionAccessor.ShowFinished));
        }

        public IActionResult Index()
        {
            var todos = _context.FindTodos(_todoSessionAccessor.SortOrder, _todoSessionAccessor.ShowFinished);
            return View("Index", new TodoListViewModel { Todos = todos,SortOrder = _todoSessionAccessor.SortOrder,  ShowFinished = _todoSessionAccessor.ShowFinished });
        }
    }
}
