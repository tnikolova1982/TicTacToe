namespace TicTacToe.PostgreSqlData.Utilities.DynamicCommand
{
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    using Dapper;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters;

    public class CommandGenerator : AbstractCommandGenerator<ProcedureParameters>
    {
        protected override ProcedureParameters GetProcedureParams(DbConnection cnn, string name)
        {
            var result = cnn.Query<ProcedureParameters>(
               "select array_agg(proargnames[t.i]) as Names, proargtypes as Types from (SELECT  p.proargnames, p.proargtypes, p.proargmodes, GENERATE_SUBSCRIPTS(proargnames,1) as i FROM pg_catalog.pg_namespace n JOIN  pg_catalog.pg_proc p ON pronamespace = n.oid WHERE nspname = :scheme AND proname = :name) t where (proargmodes[t.i] in ('i', 'o') or proargmodes is null) group by proargtypes",
               new { scheme = name.Split('.')[0], name = name.Split('.')[1] },
               commandType: CommandType.Text).SingleOrDefault();

            return result;
        }
    }
}
