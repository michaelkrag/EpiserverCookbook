using EPiServer.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MovieShop.CommenLibray.Helpers
{
    public static class SetDefaultHelper
    {
        public static void MapDefaultValues(ContentData content)
        {
            if (content == null)
            {
                return;
            }

            var propertyInfoList = content.GetType().BaseType?.GetProperties() ?? new List<PropertyInfo>().ToArray();

            foreach (var property in propertyInfoList)
            {
                var attribute = property.GetCustomAttributes(typeof(DefaultValueAttribute), true).FirstOrDefault();
                if (attribute != null)
                {
                    var defaultValueAttribute = (DefaultValueAttribute)attribute;
                    if (property.PropertyType == typeof(XhtmlString))
                    {
                        content[property.Name] = new XhtmlString(defaultValueAttribute.Value.ToString());
                    }
                    else
                    {
                        content[property.Name] = defaultValueAttribute.Value;
                    }
                }
            }
        }
    }
}