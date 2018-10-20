namespace TicTacToe
{
    using System.Web.Mvc;
    using TicTacToe.Infrastructure.Filters.CustomAuthorize;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
