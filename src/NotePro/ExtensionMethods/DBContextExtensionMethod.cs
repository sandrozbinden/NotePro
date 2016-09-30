using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotePro.Data;
using NotePro.Models;


namespace NotePro.ExtensionMethods
{
    public static class DbContextExtensionMethod
    {

        public static List<Todo> Find(this DbSet<Todo> todos, SortOrder sortOrder, bool showFinished)
        {
            switch (sortOrder)
            {
                case SortOrder.FinishDate: return todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.FinishDate).ToList();
                case SortOrder.Priority: return todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderByDescending(todo => todo.Priority).ToList();
                case SortOrder.CreatedDate: return todos.Where(todo => todo.Finished == false || todo.Finished == showFinished).OrderBy(todo => todo.CreationDate).ToList();
                default: throw new System.InvalidOperationException("Can't find sortOrder for enum: " + sortOrder);
            }
        }

        public static void DeleteAll<T>(this DbContext context)
            where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }
    }
}
