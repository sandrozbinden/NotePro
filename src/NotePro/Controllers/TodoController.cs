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
        private readonly ApplicationDbContext _dbContext; 
        private readonly ApplicationSession _session;

        public TodoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._session = new ApplicationSession(httpContextAccessor);
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
                _dbContext.Todos.Add(todo);
                _dbContext.SaveChanges();
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
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
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
                _dbContext.Todos.Update(todo);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Sort(SortOrder sortOrder)
        {
            _session.SortOrder = sortOrder;
            var todos = _dbContext.Todos.Find(_session.SortOrder, _session.ShowFinished);
            return PartialView("ListContent", todos);
        }

        [HttpPost]
        public IActionResult ToggleShowFinished()
        {
            _session.ShowFinished = !_session.ShowFinished;
            var todos = _dbContext.Todos.Find(_session.SortOrder, _session.ShowFinished);
            return PartialView("ListContent", todos);
        }

        public IActionResult ToggleLayout()
        {
            _session.DefaultLayout = !_session.DefaultLayout;
            return Index();
        }

        public IActionResult Index()
        {
            var sortOrder = _session.SortOrder;
            var showFinished = _session.ShowFinished;
            var todos = _dbContext.Todos.Find(sortOrder, showFinished);
            return View("Index", new TodoListViewModel { Todos = todos, SortOrder = sortOrder,  ShowFinished = showFinished });
        }
    }
}
