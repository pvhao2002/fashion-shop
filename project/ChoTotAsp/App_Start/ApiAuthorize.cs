using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;

        public ApiAuthorize(params string[] roles)
        {
            this._allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using(var ctx = new DBConnection())
            {
                var userId = AuthenticationUtil.GetUserId(httpContext.Request, httpContext.Session);

                if (userId == -1) return false;
                // check if user has the role to access the page
                var userRole = ctx.users.FirstOrDefault(item => item.user_id == userId)?.role;
                return !string.IsNullOrEmpty(userRole) && _allowedRoles.Contains(userRole);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new JsonResult
            {
                Data = new { success = false, message = "Unauthorized" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}