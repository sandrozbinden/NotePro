using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NotePro.ExtensionMethods
{
    public static class SessionBooleanAccessMethodExtension
    {
        public static bool GetBoolean(this ISession session, String name, bool defaultValue)
        {
            return session.GetInt32(name) == null ? defaultValue: Convert.ToBoolean(session.GetInt32(name));
        }

        public static void SetBoolean(this ISession session, String name, bool value)
        {
            session.SetInt32(name, Convert.ToInt32(value));
        }
    }
}
