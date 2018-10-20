namespace TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters
{
    using System.Data;

    public class DbParameter
    {
        /// <summary>
        ///     Create output parameter with name
        /// </summary>
        /// <param name="name">name</param>
        public DbParameter(string name)
            : this(name, null, ParameterDirection.Output)
        {
        }

        /// <summary>
        ///  Create parameter with default values
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        /// <param name="direction">diraction</param>
        /// <param name="dbType">dbType</param>
        public DbParameter(string name, object value, ParameterDirection direction = ParameterDirection.Input, DbType? dbType = null)
        {
            Name = name;
            DbType = dbType;
            Direction = direction;
            Value = value;
        }

        public string Name { get; set; }

        public DbType? DbType { get; set; }

        public ParameterDirection Direction { get; set; }

        public object Value { get; set; }
    }
}
