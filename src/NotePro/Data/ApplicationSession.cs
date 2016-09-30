using Microsoft.AspNetCore.Http;
using NotePro.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotePro.Models.TodoViewModels;

namespace NotePro.Controllers
{
    public class ApplicationSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationSession(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public bool DefaultLayout
        {
            get { return Session.GetBoolean("Application.DefaultLayout", true); }
            set { Session.SetBoolean("Application.DefaultLayout", value); }
        }

        public SortOrder SortOrder
        {
            get { return Session.GetString("Todos.SortOrder").ToEnum(Models.TodoViewModels.SortOrder.FinishDate); }
            set { Session.SetString("Todos.SortOrder", value.ToString()); }
        }

        public bool ShowFinished
        {
            get { return Session.GetBoolean("Todos.ShowFinished", true); }
            set { Session.SetBoolean("Todos.ShowFinished", value); }
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;
    }
}
