namespace TicTacToe.PostgreSqlData.Utilities.Reader
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using TicTacToe.PostgreSqlData.Utilities.PropertiesCache;

    public static class SqlReader
    {
        public static IEnumerable<T> Read<T>(this DbCommand command)
        {
            var list = new List<T>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (typeof(T).IsPrimitive())
                        {
                            T readerValue = (T)reader.GetValue(0);

                            list.Add(readerValue);
                        }
                        else
                        {
                            var instance = GetInstance<T>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string name = reader.GetName(i);
                                object value;
                                object readerValue = reader.GetValue(i);

                                value = readerValue == DBNull.Value ? null : readerValue;

                                SetPropertyValue(instance, value, new Queue<string>(name.Split('_')));
                            }

                            list.Add(instance);
                        }
                    }

                    reader.NextResult();
                }
            }

            return list;
        }

        private static void SetPropertyValue(object instance, object value, Queue<string> names)
        {
            if (names.Count == 0)
            {
                return;
            }

            var properties = PropertyInfoCache.GetPropertyInfoCache(instance.GetType());

            string firstName = names.Peek();

            var prop = properties.FirstOrDefault(p => p.Name.Equals(firstName, StringComparison.OrdinalIgnoreCase));

            if (prop == null)
            {
                return;
            }

            if (names.Count == 1)
            {
                if (prop.PropertyType.IsPrimitive() || prop.PropertyType.IsNullable())
                {
                    prop.SetValue(instance, value);

                    return;
                }
                else
                {
                    return;
                }
            }

            if (prop.GetValue(instance) == null)
            {
                var newInstance = GetInstance(prop.PropertyType);
                prop.SetValue(instance, newInstance);
            }

            names.Dequeue();

            SetPropertyValue(prop.GetValue(instance), value, names);
        }

        private static bool IsPrimitive(this Type type)
        {
            if (type == typeof(string))
            {
                return true;
            }

            return type.IsValueType || type.IsPrimitive;
        }

        private static bool IsNullable(this Type type)
        {
            if (!type.IsValueType)
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }

            return false;
        }

        private static T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        private static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
