using NotePro.Data;
using NotePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Migrations
{
    public class ApplicationDbContextSeedData
    {
        private ApplicationDbContext context;

        public ApplicationDbContextSeedData(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void SeedData()
        {
            var todos = new List<Todo>
            {
                    new Todo
                    {
                        Title = "Call my wife",
                        Text = "Organise dinner party with friends",
                        Priority = 4,
                        FinishDate =  new DateTime(DateTime.Now.Year, 5, 15)
                    }
            };
            context.AddRange(todos);
            context.SaveChanges();
        }

    }
}
