using Microsoft.AspNetCore.Http;
using NotePro.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotePro.Models.TodoViewModels;

namespace NotePro.Controllers
{
    public class TodoSessionAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoSessionAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public bool DefaultLayout
        {
            get { return GetSession().GetBoolean("Application.DefaultLayout", true); }
            set { GetSession().SetBoolean("Application.DefaultLayout", value); }
        }

        public SortOrder SortOrder
        {
            get { return GetSession().GetString("Todos.SortOrder").ToEnum(Models.TodoViewModels.SortOrder.FinishDate); }
            set { GetSession().SetString("Todos.SortOrder", value.ToString()); }
        }

        public bool ShowFinished
        {
            get { return GetSession().GetBoolean("Todos.ShowFinished", true); }
            set { GetSession().SetBoolean("Todos.ShowFinished", value); }
        }

        private ISession GetSession()
        {
            return _httpContextAccessor.HttpContext.Session;
        }
    }
}
