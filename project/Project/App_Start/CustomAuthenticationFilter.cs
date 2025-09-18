using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Project.Entity;
using Project.Utils;

namespace Project
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var userId = AuthenticationUtil.GetUserId(filterContext.HttpContext.Request, filterContext.HttpContext.Session);
            using (var ctx = new DBConnection())
            {
                var user = ctx.users.FirstOrDefault(it => it.user_id == userId && Constant.ACTIVE.Equals(it.status));
                if (user == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
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
                        { "area", Constant.ROLE_USER },
                        { "act", Constant.OPEN_LOGIN }
                    });
            }
        }
    }
}