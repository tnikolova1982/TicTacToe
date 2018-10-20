namespace TicTacToe.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using TicTacToe.Core.Logger;
    using TicTacToe.Infrastructure.Extensions;

    public abstract class BaseController : Controller
    {
        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        protected new virtual Models.User.User User
        {
            get
            {
                return HttpContext != null ? HttpContext.User as Models.User.User : null;
            }
        }

        protected void MessageCookie(string message, string type)
        {
            HttpCookie cookie = new HttpCookie("X-Message");

            string text = message + "|" + type;

            cookie.Value = Uri.EscapeDataString(text);

            HttpContext.Response.Cookies.Add(cookie);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            Logger.Error(filterContext.Exception);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string commonError = "Имате некоректни полета!";

            if (!ModelState.IsValid)
            {
                var empty = ModelState.Keys.All(k => k != string.Empty);

                // Check if has any error to show common error message
                var count = ModelState.Keys.Any(k => ModelState[k].Errors.Count > 0);
                var total = empty && count;
                if (total)
                {
                    ModelState.AddCommonError(commonError);
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}