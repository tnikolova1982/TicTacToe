namespace TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using TicTacToe.PostgreSqlData.Utilities.PropertiesCache;

    public abstract class AbstractCommandGenerator<TProcedureParameters>
          where TProcedureParameters : IProcedureParameters
    {
        private static readonly ConcurrentDictionary<Tuple<string, Type>, CacheInfo> CommandInfoCache = new ConcurrentDictionary<Tuple<string, Type>, CacheInfo>();

        public DbCommand GenerateCommand(DbConnection cnn, string commandText, object obj = null, params DbParameter[] additionalParameters)
        {
            var dbCommand = CreateCommand(cnn, commandText);

            var type = obj == null ? null : obj.GetType();

            var info = GetOrInsertCommandCacheInfo(cnn, commandText, type);

            FillCommandWithParameters(dbCommand, info, obj, additionalParameters);

            return dbCommand;
        }

        protected abstract TProcedureParameters GetProcedureParams(DbConnection cnn, string name);

        private void FillCommandWithParameters(DbCommand dbCommand, CacheInfo info, object obj, DbParameter[] additionalParameters)
        {
            if (info.Parameters != null)
            {
                foreach (var name in info.Parameters.GetNames())
                {
                    try
                    {
                        var dbParameter = CreateDbParameterFromName(dbCommand, name, obj, additionalParameters);
                        dbCommand.Parameters.Add(dbParameter);
                    }
                    catch (MissingFieldException)
                    {
                        //// If the field is missing we assume there is a default value. If there is not a default value the db will throw a meaningful exception
                    }
                }
            }
        }

        private DbCommand CreateCommand(DbConnection cnn, string commandText)
        {
            var dbCommand = cnn.CreateCommand();
            dbCommand.CommandText = commandText;
            dbCommand.CommandType = CommandType.StoredProcedure;

            return dbCommand;
        }

        private System.Data.Common.DbParameter CreateDbParameterFromName(DbCommand dbCommand, string name, object obj, DbParameter[] additionalParameters)
        {
            var dbParameter = dbCommand.CreateParameter();

            dbParameter.ParameterName = name;

            var newParameter = additionalParameters.FirstOrDefault(p => p.Name == name);

            if (newParameter != null)
            {
                if (newParameter.DbType.HasValue)
                {
                    dbParameter.DbType = newParameter.DbType.Value;
                }

                dbParameter.Value = newParameter.Value ?? DBNull.Value;
                dbParameter.Direction = newParameter.Direction;
            }
            else
            {
                dbParameter.Value = GetValueForName(obj, name) ?? DBNull.Value;
                dbParameter.Direction = ParameterDirection.Input;
            }

            return dbParameter;
        }

        private object GetValueForName(object obj, string name)
        {
            name = name.Remove(0, 1); //// Removes the leading "p" in the parameter Mapex specific

            var match = name.Split('_');

            if (obj == null)
            {
                return null;
            }

            var tempValue = GetPropertyValueRec(obj, match.First(), match.Skip(1));

            if (tempValue == null)
            {
                return null;
            }

            object value = PerepareForDb(tempValue);

            return value;
        }

        private object PerepareForDb(object value)
        {
            if (value == null)
            {
                return null;
            }

            var type = value.GetType();

            if (IsEnumOrNullableEnum(type))
            {
                var nullableEnumUnderlyingType = Nullable.GetUnderlyingType(type);

                if (nullableEnumUnderlyingType == null)
                {
                    return (int)Convert.ChangeType(value, Enum.GetUnderlyingType(type));
                }
                else
                {
                    return (int?)Convert.ChangeType(value, Enum.GetUnderlyingType(Nullable.GetUnderlyingType(type)));
                }
            }
            else
            {
                return value;
            }
        }

        private object GetPropertyValueRec(object obj, string head, IEnumerable<string> tail)
        {
            var pi = PropertyInfoCache.GetPropertyInfoCache(obj.GetType()).FirstOrDefault(p => p.Name.Equals(head, StringComparison.OrdinalIgnoreCase));

            if (pi == null)
            {
                throw new MissingFieldException(head);
            }

            object innerObj = pi.GetValue(obj, null);

            if (innerObj == null || !tail.Any())
            {
                return innerObj;
            }
            else
            {
                return GetPropertyValueRec(innerObj, tail.First(), tail.Skip(1));
            }
        }

        private CacheInfo GetOrInsertCommandCacheInfo(DbConnection cnn, string commandText, Type objType)
        {
            var info = new CacheInfo();

            var cacheKey = new Tuple<string, Type>(commandText, objType);

            if (CommandInfoCache.ContainsKey(cacheKey))
            {
                var cahceValue = CommandInfoCache[cacheKey];
                info.Parameters = cahceValue.Parameters;
            }
            else
            {
                info.Parameters = GetProcedureParams(cnn, commandText);
                CommandInfoCache.TryAdd(cacheKey, info);
            }

            return info;
        }

        private bool IsEnumOrNullableEnum(Type t)
        {
            var u = Nullable.GetUnderlyingType(t);
            return t.IsEnum || ((u != null) && u.IsEnum);
        }
    }
}
