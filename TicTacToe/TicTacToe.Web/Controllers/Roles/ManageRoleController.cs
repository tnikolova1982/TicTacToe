namespace TicTacToe.Controllers.Roles
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Filters.CustomAuthorize;
    using TicTacToe.Models.Role;
    using TicTacToe.Roles;
    using TicTacToe.Models.ViewModels;

    [CustomAuthorize(Roles = UserRightConstants.MenuRoles)]
    public class ManageRoleController : BaseController
    {
        private readonly IRoleService roleService;
        private readonly IContextManager contextManager;
        private readonly IMapperResolverService mapperResolverService;

        public ManageRoleController(ILogger logger, IContextManager contextManager, IMapperResolverService mapperResolverService, IRoleService roleService)
          : base(logger)
        {
            this.contextManager = contextManager;
            this.mapperResolverService = mapperResolverService;
            this.roleService = roleService;
        }

        [HttpGet]
        [CustomAuthorize(Roles = UserRightConstants.CreateRole + " | " + UserRightConstants.EditRole)]
        public ActionResult Upsert(Guid? id)
        {
            RoleViewModel roleModel;

            if (id.HasValue)
            {
                using (var transaction = contextManager.NewTransaction())
                {
                    var role = roleService.GetRole(id.Value);
                    roleModel = mapperResolverService.Resolver<Role, RoleViewModel>(role);
                }
            }
            else
            {
                roleModel = new RoleViewModel();
            }

            PrepareViewBag();

            return View(roleModel);
        }

        [HttpPost]
        [CustomAuthorize(Roles = UserRightConstants.CreateRole + " | " + UserRightConstants.EditRole)]
        public ActionResult Upsert(RoleViewModel model)
        {
            ValidateModel(model);

            if (ModelState.IsValid)
            {
                using (var transaction = contextManager.NewTransaction())
                {
                    var roleModel = mapperResolverService.Resolver<RoleViewModel, Role>(model);

                    roleService.UpsertRole(roleModel);

                    transaction.Commit();

                    MessageCookie("Успешен запис!", "success");
                    return RedirectToAction("Index", "Role");
                }
            }
            else
            {
                PrepareViewBag();
                return View(model);
            }
        }

        private void ValidateModel(RoleViewModel model)
        {
            using (contextManager.NewTransaction())
            {
                var result = roleService.SearchRole(new RoleQueryModel { Name = model.Name });

                if (result != null && result.Any(e => e.Name.Contains(model.Name)))
                {
                    ModelState.AddModelError("Name", "Съществува роля с такова име!");
                }
            }
        }

        private void PrepareViewBag()
        {
            using (contextManager.NewTransaction())
            {
                ViewBag.AllActivities = roleService.GetAllActivities();
            }
        }
    }
}