using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Models.TodoViewModels
{
    public class TodoListViewModel
    {
        public List<Todo> Todos { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool ShowFinished { get; set; }
    }

    public enum SortOrder
    {
        FinishDate, CreatedDate, Priority
    }

    public static class SortOrderExtensions
    {
        public static String GetName(this SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.CreatedDate: return "By created Date";
                case SortOrder.FinishDate: return "By finished Date";
                case SortOrder.Priority: return "By Priority";
                default: throw new System.InvalidOperationException("Can't find sortOrder for enum: " + sortOrder); ;
            }
        }
    }
}
