using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NotePro.Models;
using Microsoft.EntityFrameworkCore;
using NotePro.Models.TodoViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotePro.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public virtual DbSet<Todo> Todos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public List<Todo> FindTodos(SortOrder sortOrder, bool showFinished)
        {
            switch (sortOrder)
            {
                case SortOrder.FinishDate: return Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.FinishDate).ToList();
                case SortOrder.Priority: return Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderByDescending(todo => todo.Priority).ToList();
                case SortOrder.CreatedDate: return Todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.CreationDate).ToList();
                default: throw new System.InvalidOperationException("Can't find sortOrder for enum: " + sortOrder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
} 
