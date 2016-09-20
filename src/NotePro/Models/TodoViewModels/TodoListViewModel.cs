using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Models.TodoViewModels
{
    public class TodoListViewModel
    {
        public List<Todo> Todos { get; set; }
        public String SortOrder { get; set; }
        public bool ShowFinished { get; set; }
    }
}
