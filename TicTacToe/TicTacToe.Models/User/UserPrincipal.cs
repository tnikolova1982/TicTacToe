namespace TicTacToe.Models.User
{
    using System.Linq;
    using System.Security.Principal;

    public class UserPrincipal : DomainEntity, IPrincipal
    {
        private IIdentity identity;
        private string[] roleActivities;

        public string UserName { get; set; }

        public string[] RoleActivities
        {
            get { return roleActivities ?? (roleActivities = new string[1]); }

            set { roleActivities = value; }
        }

        public IIdentity Identity
        {
            get { return identity ?? (identity = new GenericIdentity(UserName)); }
        }

        public bool IsInRole(string role)
        {
            return RoleActivities.Contains(role);
        }
    }
}
