namespace TicTacToe.Data
{
    using System;
    using System.Collections.Generic;
    using TicTacToe.Models.Role;

    public interface IRoleRepository
    {
        IEnumerable<RoleTableModel> SearchRole(RoleQueryModel model);

        IEnumerable<Activity> GetAllActivities();

        void UpsertRole(Role model);

        Role GetRole(Guid roleId);

        IEnumerable<Guid> GetUserRoles(Guid id);
    }
}
