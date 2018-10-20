namespace TicTacToe.Controllers.User
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Filters.CustomAuthorize;
    using TicTacToe.Models;
    using TicTacToe.Models.User;
    using TicTacToe.Roles;

    [CustomAuthorize(Roles = UserRightConstants.MenuUsers)]
    public class UserController : BaseController, ISearchable<UserQueryModel, UserTableModel>
    {
        private readonly IContextManager contextManager;
        private readonly ISearchTableService searchTableService;
        private readonly IUserService userService;

        public UserController(ILogger logger, IContextManager contextManager, ISearchTableService searchTableService, IUserService userService)
            : base(logger)
        {
            this.contextManager = contextManager;
            this.searchTableService = searchTableService;
            this.userService = userService;
        }

        public ActionResult Index(UserQueryModel model)
        {
            PrepareInitialQuery(model);

            return View(model);
        }

        public ActionResult FindResult(UserQueryModel model)
        {
            var result = searchTableService.Search(model, this);

            return PartialView(result);
        }

        public IEnumerable<UserTableModel> Search(UserQueryModel model)
        {
            using (contextManager.NewTransaction())
            {
                return userService.SearchUsers(model);
            }
        }

        public int CountResults(UserQueryModel model)
        {
            using (contextManager.NewTransaction())
            {
                var result = userService.SearchUsers(model);

                return result.Count();
            }
        }

        protected UserQueryModel PrepareInitialQuery(UserQueryModel model)
        {
            IEnumerable<Nomenclature> roleIds;

            using (contextManager.NewTransaction())
            {
                roleIds = userService.GetAllRoles();
            }

            var roleIdDropDown = roleIds.Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString()));

            model.RoleIdDropDown = roleIdDropDown;

            return model;
        }
    }
}