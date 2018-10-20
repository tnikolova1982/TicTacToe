namespace TicTacToe.Controllers.Account
{
    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Models.ViewModels;
    using System;
    using System.Web.Mvc;
    using System.Web.Security;

    public class AccountController : BaseController
    {
        private readonly IContextManager contextManager;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AccountController(ILogger logger, IContextManager contextManager, IUserService userService, IRoleService roleService)
           : base(logger)
        {
            this.contextManager = contextManager;
            this.userService = userService;
            this.roleService = roleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return User != null ? RedirectToLocal(returnUrl) : View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (User != null && User.IsActive)
            {
                return RedirectToLocal(returnUrl);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                Models.User.User user;

                using (contextManager.NewTransaction())
                {
                    user = userService.FindUserId(model.Username, model.Password);

                    if (!user.Id.HasValue)
                    {
                        throw new ArgumentException();
                    }

                    user = userService.GetUser(user.Id.Value);
                    user.RoleActivities = roleService.GetUserRoles(user.Id.Value);
                }

                if (user == null)
                {
                    ModelState.AddModelError("Username", "Потребителското име не е активирано!");
                    return View(model);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError("Username", "Потребителското име е блокирано!");
                    return View(model);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    Session.Timeout = (int)FormsAuthentication.Timeout.TotalMinutes;

                    Session["USER"] = user;
                }

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") &&
                    !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return RedirectToLocal(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Logger.Warning("Неуспешен вход за потребител " + model.Username);
                Logger.Error(e);
                ModelState.AddModelError("Username", "Грешно име или парола.");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("LogIn", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}