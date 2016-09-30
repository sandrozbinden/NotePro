using Microsoft.AspNetCore.Http;
using NotePro.ExtensionMethods;
using NotePro.Models;

namespace NotePro.Data
{
    public class ApplicationSession
    {
        private readonly ISession _session;

        public ApplicationSession(IHttpContextAccessor httpContextAccessor) : this (httpContextAccessor.HttpContext.Session)
        {
            
        }

        public ApplicationSession(ISession session)
        {
            this._session = session;
        }

        public bool DefaultLayout
        {
            get { return _session.GetBoolean("Application.DefaultLayout", true); }
            set { _session.SetBoolean("Application.DefaultLayout", value); }
        }

        public SortOrder SortOrder
        {
            get { return _session.GetString("Todos.SortOrder").ToEnum(SortOrder.FinishDate); }
            set { _session.SetString("Todos.SortOrder", value.ToString()); }
        }

        public bool ShowFinished
        {
            get { return _session.GetBoolean("Todos.ShowFinished", true); }
            set { _session.SetBoolean("Todos.ShowFinished", value); }
        }
       
    }
}
