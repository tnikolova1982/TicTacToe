
namespace TicTacToe.PostgreSqlData.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TicTacToe.Core.Logger;
    using TicTacToe.Data;
    using TicTacToe.Data.Context;
    using TicTacToe.Models;
    using TicTacToe.Models.User;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand;
    using TicTacToe.PostgreSqlData.Utilities.DynamicCommand.Parameters;
    using TicTacToe.PostgreSqlData.Utilities.Query;

    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(ILogger logger, IContext context)
          : base(logger, context)
        {
        }

        public IEnumerable<UserTableModel> SearchUsers(UserQueryModel query)
        {
            return Context.Connection.Query<UserTableModel>("admdata.search_user", query);
        }

        public User GetUser(Guid userid)
        {
            return Context.Connection.Query<User>("admdata.get_user", new { userid }).SingleOrDefault();
        }

        public Guid InsertUser(User user)
        {
            using (var command = Context.Connection.GenerateCommand("admdata.ins_user", user, new DbParameter("pid")))
            {
                command.ExecuteNonQuery();

                return (Guid)command.Parameters["pid"].Value;
            }
        }

        public void UpdateUser(User user)
        {
            using (var command = Context.Connection.GenerateCommand("admdata.upd_user", user))
            {
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(Guid id)
        {
            using (var command = Context.Connection.GenerateCommand("admdata.del_user", id, new DbParameter("pid", id)))
            {
                command.ExecuteNonQuery();
            }
        }

        public User FindUserId(string userName, string password)
        {
            return Context.Connection.Query<User>("admdata.get_userbyusernameandpass", new { userName, password }).SingleOrDefault();
        }

        public IEnumerable<Nomenclature> GetAllRoles()
        {
            return Context.Connection.Query<Nomenclature>("admdata.get_roles");
        }

        public bool VerifyUserName(string userName)
        {
            return Context.Connection.Query<bool>("admdata.get_userbyusername", new { userName }).SingleOrDefault();
        }
    }
}
