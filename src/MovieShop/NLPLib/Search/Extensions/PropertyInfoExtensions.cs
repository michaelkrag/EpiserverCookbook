using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLPLib.Search.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string FullName(this PropertyInfo propertyInfo)
        {
            return $"{propertyInfo.DeclaringType.FullName}.{propertyInfo.Name}";
        }
    }
}