namespace TicTacToe.Data
{
    using System;
    using System.Collections.Generic;
    using TicTacToe.Models;
    using TicTacToe.Models.User;

    public interface IUserRepository
    {
        IEnumerable<UserTableModel> SearchUsers(UserQueryModel query);

        User GetUser(Guid id);

        Guid InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid id);

        User FindUserId(string userName, string password);

        IEnumerable<Nomenclature> GetAllRoles();

        bool VerifyUserName(string userName);
    }
}
