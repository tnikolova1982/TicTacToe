namespace TicTacToe.PostgreSqlData.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TicTacToe.Core.Logger;
    using TicTacToe.Data;
    using TicTacToe.Data.Context;
    using TicTacToe.Models.Role;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters;
    using TicTacToe.PostgreSqlData.Utilities.Query;

    public class RoleRepository : Repository, IRoleRepository
    {
        public RoleRepository(ILogger logger, IContext context)
            : base(logger, context)
        {
        }

        public IEnumerable<RoleTableModel> SearchRole(RoleQueryModel query)
        {
            return Context.Connection.Query<RoleTableModel>("admdata.search_role", query);
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return Context.Connection.Query<Activity>("admdata.get_ntables", new { tablename = "nactivity" });
        }

        public void UpsertRole(Role model)
        {
            using (var command = Context.Connection.GenerateCommand(
                "admdata.upsert_role",
                model,
                new DbParameter("pid", model.IsNew() ? null : (Guid?)model.Id),
                new DbParameter("pactivities", model.ActivitiesIds.ToArray())))
            {
                command.ExecuteNonQuery();
            }
        }

        public Role GetRole(Guid roleId)
        {
            return Context.Connection.Query<Role>("admdata.get_role", new { id = roleId }).SingleOrDefault();
        }

        public IEnumerable<Guid> GetUserRoles(Guid id)
        {
            return Context.Connection.Query<Guid>("admdata.get_useractivities", new { userid = id });
        }
    }
}
