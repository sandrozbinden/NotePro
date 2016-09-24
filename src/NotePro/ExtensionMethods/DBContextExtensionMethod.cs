using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.ExtensionMethods
{
    public static class DbContextExtensionMethod
    {

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
