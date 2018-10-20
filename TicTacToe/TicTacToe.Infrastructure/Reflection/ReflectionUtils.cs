namespace TicTacToe.Infrastructure.Reflection
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using TicTacToe.Infrastructure.Extensions;
    using TicTacToe.Infrastructure.Misc.IgnoreProperty;

    public static class ReflectionUtils
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> propertyInfoCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        ///     Get caller class name
        /// </summary>
        public static string CallClassName
        {
            get
            {
                var declaringType = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
                return declaringType != null ? declaringType.FullName : string.Empty;
            }
        }

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

        /// <summary>
        ///     Trims all the string/String props of an Object.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj"></param>
        public static void TrimObjectProps<T>(T obj)
            where T : class
        {
            MapObjectStrings(obj, v => v.TrimAndNullifyEmptyStrings());
        }

        public static T UpperObjectProps<T>(T obj)
           where T : class
        {
            MapObjectStrings(obj, v => v.ToUpper());
            return obj;
        }

        public static void MapObjectStrings<T>(T obj, Func<string, string> eval)
           where T : class
        {
            if (obj == null)
            {
                return;
            }

            var stringProperties = GetStringProperties(obj);

            foreach (var stringProperty in stringProperties)
            {
                var val = stringProperty.GetValue(obj, null) as string;
                if (val != null)
                {
                    stringProperty.SetValue(obj, eval(val), null);
                }
            }
        }

        /// <summary>
        ///     Adds search extensions all the string/String props of an Object.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj"></param>
        public static void ExtendSearchObjectProps<T>(T obj)
        {
            foreach (var prop in GetPropertyInfoCache(obj.GetType()))
            {
                try
                {
                    if (prop.PropertyType.Name.ToLower() == "string")
                    {
                        var val = (string)prop.GetValue(obj, null);
                        if (!string.IsNullOrEmpty(val))
                        {
                            var trim = string.Format("%{0}%", val.Replace('*', '%').Replace(' ', '%')).Trim();
                            prop.SetValue(obj, trim, null);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///     Trims all the string/String props of an Object and adds search extensions all the string/String props of the Object.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj"></param>
        public static void TrimAndExtendObjectProps<T>(T obj)
            where T : class
        {
            TrimObjectProps(obj);
            ExtendSearchObjectProps(obj);
        }

        /// <summary>
        ///     Filters the list by a given filter.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="filter"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> FilterList<T>(T filter, IEnumerable<T> list)
        {
            return FilterList(filter, list, new HashSet<string>());
        }

        /// <summary>
        ///     Filters the list by a given filter.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="filter"></param>
        /// <param name="list"></param>
        /// <param name="blackSet"></param>
        /// <returns></returns>
        public static IEnumerable<T> FilterList<T>(T filter, IEnumerable<T> list, HashSet<string> blackSet)
        {
            var filteredList = new List<T>();

            var isFilterEmpty = 0;

            list.Each(
                delegate (T item)
                {
                    var filterMatch = true;
                    var enteredField = false;

                    var objectType = typeof(T);
                    var typeProperties = objectType.GetProperties().Where(prop => !prop.IsDefined(typeof(IgnoreProperty), true)).ToArray();

                    foreach (var tempPropertyInfo in typeProperties)
                    {
                        var propertyInfo = tempPropertyInfo;
                        if (blackSet == null || !blackSet.Contains(propertyInfo.Name))
                        {
                            var val = propertyInfo.GetValue(item, null);
                            var filterVal = propertyInfo.GetValue(filter, null);

                            if (val != null && filterVal != null)
                            {
                                enteredField = true;
                                filterMatch = val.ToString().IndexOf(filterVal.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;
                                if (!filterMatch)
                                {
                                    break;
                                }
                            }
                            else if (filterVal != null)
                            {
                                filterMatch = false;
                                break;
                            }
                        }
                    }

                    if (enteredField)
                    {
                        if (filterMatch)
                        {
                            filteredList.Add(item);
                        }
                    }
                    else
                    {
                        isFilterEmpty++;
                    }
                });

            if (list.Count() - isFilterEmpty == 0)
            {
                return list;
            }

            return filteredList;
        }

        /// <summary>
        /// Checks if given object has properties that are not null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj"></param>
        /// <returns>True if obj has property with value different from null, Flase otherwise</returns>
        public static bool HasNotNullProperty<T>(T obj)
        {
            return GetPropertyInfoCache(typeof(T))
                .Select(prop => prop.GetValue(obj, null))
                .Any(val => val != null);
        }

        /// <summary>
        /// Checks if given object has properties that are not null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj"></param>
        /// <returns>True if obj has property with value different from null, Flase otherwise</returns>
        public static bool HasNotNullOrEmptyProperty<T>(T obj)
        {
            return GetPropertyInfoCache(typeof(T))
                .Select(prop => prop.GetValue(obj, null))
                .Any(val => val is string ? !string.IsNullOrEmpty(val as string) : val != null);
        }

        public static object GetPropValue(this object src, string propName)
        {
            if (string.IsNullOrWhiteSpace(propName))
            {
                throw new ArgumentException("Pass a valid property name, not an empty string.");
            }

            if (GetPropertyInfoCache(src.GetType()).All(p => p.Name != propName))
            {
                throw new ArgumentException("No property named \"" + propName + "\", was found in object.");
            }

            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetNestedPropertyValue(this object target, string compoundProperty, object value)
        {
            if (string.IsNullOrWhiteSpace(compoundProperty))
            {
                throw new ArgumentException("Pass a valid property name, not an empty string.");
            }

            string[] bits = compoundProperty.Split('.');
            for (int i = 0; i < bits.Length - 1; i++)
            {
                PropertyInfo propertyToGet = target.GetType().GetProperty(bits[i]);
                target = propertyToGet.GetValue(target, null);
            }

            PropertyInfo propertyToSet = target.GetType().GetProperty(bits.Last());
            propertyToSet.SetValue(target, value, null);
        }

        public static object GetNestedPropertyValue(this object src, string compoundProperty)
        {
            if (string.IsNullOrWhiteSpace(compoundProperty))
            {
                throw new ArgumentException("Pass a valid property name, not an empty string.");
            }

            string[] bits = compoundProperty.Split('.');
            for (int i = 0; i < bits.Length - 1; i++)
            {
                PropertyInfo propertyToGet = src.GetType().GetProperty(bits[i]);
                src = propertyToGet.GetValue(src, null);
            }

            return src.GetType().GetProperty(bits.Last()).GetValue(src, null);
        }

        public static Dictionary<PropertyInfo, string> GetNonEmptyDateProperties<T>(this T obj, NameValueCollection properties)
        {
            var dateProps = new Dictionary<PropertyInfo, string>();
            for (var i = 0; i < properties.Count; i++)
            {
                var requestKey = properties.Keys[i].Replace(".FilterText", string.Empty);

                if (!string.IsNullOrEmpty(properties[i]) && obj.GetType().GetProperty(requestKey) != null &&
                    (obj.GetType().GetProperty(requestKey).PropertyType.FullName == typeof(DateTime).FullName
                     || obj.GetType().GetProperty(requestKey).PropertyType.FullName == typeof(DateTime?).FullName))
                {
                    dateProps.Add(obj.GetType().GetProperty(requestKey), properties[i]);
                }
            }

            return dateProps;
        }

        private static IEnumerable<PropertyInfo> GetStringProperties<T>(T obj)
        {
            // Get only read/write string properties
            return GetPropertyInfoCache(obj.GetType())
                 .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);
        }

        private static string TrimAndNullifyEmptyStrings(this string s)
        {
            if (s != null)
            {
                s = s.Trim();
            }

            if (s == string.Empty)
            {
                s = null;
            }

            return s;
        }
    }
}
