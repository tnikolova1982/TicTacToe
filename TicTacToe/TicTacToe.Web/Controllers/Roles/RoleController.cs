namespace TicTacToe.Controllers.Roles
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Filters.CustomAuthorize;
    using TicTacToe.Models.Role;
    using TicTacToe.Roles;
   

    [CustomAuthorize(Roles = UserRightConstants.MenuRoles)]
    public class RoleController : BaseController, ISearchable<RoleQueryModel, RoleTableModel>
    {
        private readonly IContextManager contextManager;
        private readonly ISearchTableService searchTableService;
        private readonly IRoleService roleService;

        public RoleController(ILogger logger, IContextManager contextManager, ISearchTableService searchTableService, IRoleService roleService)
            : base(logger)
        {
            this.contextManager = contextManager;
            this.searchTableService = searchTableService;
            this.roleService = roleService;
        }

        public ActionResult Index(RoleQueryModel model = null)
        {
            return View(model);
        }

        public ActionResult FindResult(RoleQueryModel model)
        {
            var result = searchTableService.Search(model, this);

            return PartialView(result);
        }

        public IEnumerable<RoleTableModel> Search(RoleQueryModel model)
        {
            using (contextManager.NewTransaction())
            {
                return roleService.SearchRole(model);
            }
        }

        public int CountResults(RoleQueryModel model)
        {
            using (contextManager.NewTransaction())
            {
                var result = roleService.SearchRole(model);

                return result.Count();
            }
        }
    }
}