namespace TicTacToe.Infrastructure.Filters.CustomAuthorize
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// http://stackoverflow.com/questions/22708727/asp-net-identity-check-for-multiple-roles-to-get-access
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /*
     *  Exp -> SubExp '&' SubExp // AND
     *  Exp -> SubExp '|' SubExp // OR
     *  Exp -> SubExp '^' SubExp // XOR
     *  SubExp -> '(' Exp ')'
     *  SubExp -> '!' Exp        // NOT
     *  SubExp -> RoleName
     *  RoleName -> [a-z]
     */


        private const char TokenSeparator = ' ';
        private const char TokenAnd = '&';
        private const char TokenOr = '|';
        private const char TokenXor = '^';
        private const char TokenNot = '!';
        private const char TokenParentheseOpen = '(';
        private const char TokenParentheseClose = ')';


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContext.Current.User = HttpContext.Current != null && HttpContext.Current.Session != null ? HttpContext.Current.Session["USER"] as IPrincipal : null;
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                // If a child action cache block is active, we need to fail immediately, even if authorization
                // would have succeeded. The reason is that there's no way to hook a callback to rerun
                // authorization before the fragment is served from the cache, so we can't guarantee that this
                // filter will be re-run on subsequent requests.
                throw new InvalidOperationException("Невалидна операция!");
            }

            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
                                        typeof(AllowAnonymousAttribute), true);

            if (skipAuthorization)
            {
                return;
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                var cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var user = HttpContext.Current.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Roles))
            {
                return true;
            }

            // Calculate expression value
            var expression = Parse(Roles);
            return expression == null || expression.Eval(user);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjax)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                var user = HttpContext.Current.User;

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = WebConfigurationManager.AppSettings[user != null ? "UnauthorizedController" : "RedirectController"],
                            action = WebConfigurationManager.AppSettings[user != null ? "UnauthorizedAction" : "RedirectAction"],
                            returnUrl = filterContext.HttpContext.Request.RawUrl,
                            username = filterContext.HttpContext.Request["username"],
                            password = filterContext.HttpContext.Request["password"]
                        }));
            }
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var isAuthorized = AuthorizeCore(httpContext);
            return isAuthorized ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

        private Node Parse(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            var delimiters = new[] { TokenSeparator, TokenAnd, TokenOr, TokenXor, TokenNot, TokenParentheseOpen, TokenParentheseClose };

            var tokens = new List<string>();
            var sb = new StringBuilder();

            foreach (var c in text.Where(c => c != TokenSeparator))
            {
                if (delimiters.ToList().Contains(c))
                {
                    if (sb.Length > 0)
                    {
                        tokens.Add(sb.ToString());
                        sb.Clear();
                    }

                    tokens.Add(c.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    sb.Append(c);
                }
            }

            if (sb.Length > 0)
            {
                tokens.Add(sb.ToString());
            }

            return Parse(tokens.ToArray());
        }

        private Node Parse(string[] tokens)
        {
            var index = 0;
            return ParseExp(tokens, ref index);
        }

        private Node ParseExp(string[] tokens, ref int index)
        {
            var leftExp = ParseSubExp(tokens, ref index);
            if (index >= tokens.Length)
            {
                return leftExp;
            }

            var token = tokens[index];

            if (token == "&")
            {
                index++;
                var rightExp = ParseSubExp(tokens, ref index);
                return new AndNode(leftExp, rightExp);
            }

            if (token == "|")
            {
                index++;
                var rightExp = ParseSubExp(tokens, ref index);
                return new OrNode(leftExp, rightExp);
            }

            if (token == "^")
            {
                index++;
                var rightExp = ParseSubExp(tokens, ref index);
                return new XorNode(leftExp, rightExp);
            }

            throw new Exception("Expected '&' or '|' or EOF");
        }

        private Node ParseSubExp(string[] tokens, ref int index)
        {
            var token = tokens[index];

            if (token == "(")
            {
                index++;
                var node = ParseExp(tokens, ref index);

                if (tokens[index] != ")")
                {
                    throw new Exception("Expected ')'");
                }

                index++; // Skip ')'

                return node;
            }

            if (token == "!")
            {
                index++;
                var node = ParseExp(tokens, ref index);
                return new NotNode(node);
            }

            index++;
            return new RoleNode(token);
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}
