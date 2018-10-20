namespace TicTacToe.Controllers.User
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Extensions;
    using TicTacToe.Infrastructure.Filters.CustomAuthorize;
    using TicTacToe.Models.ViewModels;
    using TicTacToe.Roles;

    [CustomAuthorize(Roles = UserRightConstants.MenuUsers)]
    public class ManageUserController : BaseController
    {
        private readonly IContextManager contextManager;

        private readonly IMapperResolverService mapperResolverService;

        private readonly IUserService userService;


        public ManageUserController(ILogger logger, IMapperResolverService mapperResolverService, IContextManager contextManager, IUserService userService)
           : base(logger)
        {
            this.contextManager = contextManager;
            this.mapperResolverService = mapperResolverService;
            this.userService = userService;
        }


        [HttpGet]
        [CustomAuthorize(Roles = UserRightConstants.CreateUser)]
        public ActionResult CreateUser(Guid? id)
        {
            CreateUserViewModel model;

            if (id.HasValue)
            {
                MessageCookie("Невалидна операция!", "danger");
                return RedirectToAction("Index", "User");
            }
            else
            {
                model = new CreateUserViewModel();
            }

            PrepareViewBag();

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = UserRightConstants.CreateUser)]
        public ActionResult CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userId;

                using (var transaction = contextManager.NewTransaction())
                {
                    var userModel = mapperResolverService.Resolver<CreateUserViewModel, Models.User.User>(model);

                    userId = userService.UpsertUser(userModel);

                    transaction.Commit();
                }

                TempData["isRedirect"] = true;
                MessageCookie("Успешен запис!", "success");

                return RedirectToAction("Index", "User");
            }

            PrepareViewBag();

            return View(model);
        }

        [HttpGet]
        [CustomAuthorize(Roles = UserRightConstants.EditUser)]
        public ActionResult EditUser(Guid? id)
        {
            EditUserViewModel model;

            if (id.HasValue)
            {
                using (contextManager.NewTransaction())
                {
                    var user = userService.GetUser(id.Value);

                    model = mapperResolverService.Resolver<Models.User.User, EditUserViewModel>(user);
                }
            }
            else
            {
                MessageCookie("Невалидна операция!", "danger");
                return RedirectToAction("Index", "User");
            }

            PrepareViewBag();

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize(Roles = UserRightConstants.EditUser)]
        public ActionResult EditUser(EditUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var transaction = contextManager.NewTransaction())
                    {
                        var userModel = mapperResolverService.Resolver<EditUserViewModel, Models.User.User>(model);

                        userService.UpsertUser(userModel);

                        transaction.Commit();
                    }

                    MessageCookie("Успешна редакция!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    PrepareViewBag();

                    return View(model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                PrepareViewBag();
                return View(model);
            }
        }

        public ActionResult VerifyUserName(string userName)
        {
            bool isExist;

            using (contextManager.NewTransaction())
            {
                isExist = userService.VerifyUserName(userName);
            }

            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        private void PrepareViewBag()
        {
            using (contextManager.NewTransaction())
            {
                var list = userService.GetAllRoles().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
                list.AddEmptyDropDownOption();

                ViewBag.UserRoles = list;
            }
        }
    }
}