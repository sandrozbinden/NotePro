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
        private readonly ISession _session;

        public ApplicationSession(IHttpContextAccessor httpContextAccessor)
        {
            this._session = httpContextAccessor.HttpContext.Session;
        }

        public bool DefaultLayout
        {
            get { return _session.GetBoolean("Application.DefaultLayout", true); }
            set { _session.SetBoolean("Application.DefaultLayout", value); }
        }

        public SortOrder SortOrder
        {
            get { return _session.GetString("Todos.SortOrder").ToEnum(Models.TodoViewModels.SortOrder.FinishDate); }
            set { _session.SetString("Todos.SortOrder", value.ToString()); }
        }

        public bool ShowFinished
        {
            get { return _session.GetBoolean("Todos.ShowFinished", true); }
            set { _session.SetBoolean("Todos.ShowFinished", value); }
        }

    }
}
