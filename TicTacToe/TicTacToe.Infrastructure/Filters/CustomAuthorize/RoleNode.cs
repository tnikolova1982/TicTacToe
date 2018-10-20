namespace TicTacToe.Infrastructure.Filters.CustomAuthorize
{
    using System.Security.Principal;

    internal class RoleNode : Node
    {
        private readonly string roleName;

        public RoleNode(string roleName)
        {
            this.roleName = roleName;
        }

        public string RoleName
        {
            get { return roleName; }
        }

        public override bool Eval(IPrincipal principal)
        {
            return principal.IsInRole(RoleName);
        }
    }
}
