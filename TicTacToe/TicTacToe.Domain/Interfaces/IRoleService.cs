namespace TicTacToe.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using TicTacToe.Models.Role;

    public interface IRoleService
    {
        IEnumerable<RoleTableModel> SearchRole(RoleQueryModel model);

        IEnumerable<Activity> GetAllActivities();

        void UpsertRole(Role model);

        Role GetRole(Guid roleId);

        string[] GetUserRoles(Guid id);
    }
}
