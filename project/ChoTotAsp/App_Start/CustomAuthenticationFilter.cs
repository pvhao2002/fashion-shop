using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using ChoTotAsp.Utils;

namespace ChoTotAsp
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var userId = AuthenticationUtil.GetUserId(filterContext.HttpContext.Request, filterContext.HttpContext.Session);

            if (userId == -1)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                // Redirecting the user to the Login View of Account Controller
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Index" },
                        // return area name if you have one
                        { "area", Constant.ROLE_USER },
                        { "act", Constant.OPEN_LOGIN }
                    });
            }
        }
    }
}