using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.Extensions
{
    public static class TypeExtension
    {
        public static Type[] GetAssembly(this Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(x => x == type.Assembly).FirstOrDefault()?.GetTypes();
        }

        public static Dictionary<Type, Type> GetInterfaceWithDefaultConventions(this Type[] types)
        {
            Dictionary<Type, Type> typeDictionary = new Dictionary<Type, Type>();
            foreach (var interfaceType in types.GetInterfaces())
            {
                var imple = types.GetImplementation(interfaceType);
                if (imple != null)
                {
                    typeDictionary[interfaceType] = imple;
                }
            }
            return typeDictionary;
        }

        private static List<Type> GetInterfaces(this Type[] types)
        {
            return types.Where(x => x.IsInterface).ToList();
        }

        private static Type GetImplementation(this Type[] types, Type interfaceType)
        {
            var defaultClassName = interfaceType.DefaultClassName();

            var implemtaion = types.Where(p => interfaceType.IsAssignableFrom(p)).Where(x => x.Name == defaultClassName).FirstOrDefault();

            return implemtaion;
        }

        private static string DefaultClassName(this Type interfaceType)
        {
            return interfaceType.Name.Remove(0, 1);
        }
    }
}