using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.ExtensionMethods
{
    public static class ParseEnumExtensionMethod
    {
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            return (String.IsNullOrEmpty(value) ? defaultValue : (T)Enum.Parse(typeof(T), value, true));
        }

    }
}
