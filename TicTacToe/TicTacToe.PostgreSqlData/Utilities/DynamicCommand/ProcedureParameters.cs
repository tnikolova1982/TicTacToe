namespace TicTacToe.PostgreSqlData.Utilities.DynamicCommand
{
    public class ProcedureParameters : IProcedureParameters
    {
        protected string[] Names { get; set; }

        public string[] GetNames()
        {
            return Names;
        }
    }
}
