namespace TicTacToe.Controllers
{
    using TicTacToe.Core.Logger;
    using TicTacToe.Data.Context;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        private readonly IContextManager contextManager;

        public HomeController(ILogger logger, IContextManager contextManager)
            : base(logger)
        {
            this.contextManager = contextManager;
        }

        public ActionResult Index()
        {
            if (User != null)
            {
                return View();
            }

           return RedirectToAction("LogIn", "Account");
        }

        public ActionResult NotAuthorized()
        {
            const string Msg = "Нямате права за достъп до търсената от Вас страница!";
            return View("~/Views/Shared/Error.cshtml", null, Msg);
        }

        public ActionResult NotFound()
        {
            const string Msg = "За съжаление търсената от Вас страница не може да бъде открита!";
            return View("~/Views/Shared/Error.cshtml", null, Msg);
        }

        public ActionResult ServerError()
        {
            const string Msg = "Грешка при работа с приложението.";
            return View("~/Views/Shared/Error.cshtml", null, Msg);
        }

        [AllowAnonymous]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}