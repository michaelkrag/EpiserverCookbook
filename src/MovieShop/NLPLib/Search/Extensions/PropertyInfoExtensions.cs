using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NLPLib.Search.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static string FullName(this PropertyInfo propertyInfo)
        {
            return $"{propertyInfo.DeclaringType.FullName}.{propertyInfo.Name}";
        }

        public static IEnumerable<PropertyInfo> GetAttributeProperties<TAttribute>(this Type type)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
                if (attribute != null)
                {
                    yield return property;
                }
            }
        }

        public static string GetExpressionName<T, P>(this Expression<Func<T, P>> action)
        {
            var expression = (MemberExpression)action.Body;

            var objName = expression.Expression.Type.FullName;

            var name = expression.Member.Name;
            return $"{objName}.{name}";
        }
    }
}