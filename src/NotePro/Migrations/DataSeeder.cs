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
                        Title = "Call wife",
                        Text = "Organise dinner party with friends",
                        Priority = 4,
                        FinishDate =  new DateTime(DateTime.Now.Year, 5, 15),
                        Finished = false
                    },
                    new Todo
                    {
                        Title = "E-Mail boss",
                        Text = "Ask about c# conferences",
                        Priority = 3,
                        FinishDate =  new DateTime(DateTime.Now.Year, 5, 18),
                        Finished = false

                    },
                    new Todo
                    {
                        Title = "Call travel agency",
                        Text = "Extend vacation package (058 702 66 50)",
                        Priority = 5,
                        FinishDate =  new DateTime(DateTime.Now.Year, 5, 16),
                        Finished = true
                    }
            };
            context.AddRange(todos);
            context.SaveChanges();
        }

    }
}
