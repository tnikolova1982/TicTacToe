namespace TicTacToe.PostgreSqlData.Utilities.PropertiesCache
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;

    public class PropertyInfoCache
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> propertyInfoCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static PropertyInfo[] GetPropertyInfoCache(Type type)
        {
            if (propertyInfoCache.ContainsKey(type))
            {
                return propertyInfoCache[type];
            }
            else
            {
                var properties = type.GetProperties();
                propertyInfoCache.TryAdd(type, properties);

                return properties;
            }
        }
    }
}
