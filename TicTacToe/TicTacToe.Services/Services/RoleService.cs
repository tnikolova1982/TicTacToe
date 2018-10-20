namespace TicTacToe.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TicTacToe.Data;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Models.Role;

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<RoleTableModel> SearchRole(RoleQueryModel model)
        {
            return roleRepository.SearchRole(model);
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return roleRepository.GetAllActivities();
        }

        public void UpsertRole(Role model)
        {
            roleRepository.UpsertRole(model);
        }

        public Role GetRole(Guid roleId)
        {
            return roleRepository.GetRole(roleId);
        }

        public string[] GetUserRoles(Guid id)
        {
            return roleRepository.GetUserRoles(id).Select(e => e.ToString()).ToArray();
        }
    }
}
