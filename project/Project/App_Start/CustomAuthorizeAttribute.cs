using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Project.Entity;
using Project.Utils;

namespace Project
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this._allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            using (var ctx = new DBConnection())
            {
                var userId = AuthenticationUtil.GetUserId(httpContext.Request, httpContext.Session);

                if (userId == -1) return false;
                // check if user has the role to access the page
                var userRole = ctx.users
                    .FirstOrDefault(item => item.user_id == userId && Constant.ACTIVE.Equals(item.status))?.role;
                return !string.IsNullOrEmpty(userRole) && _allowedRoles.Contains(userRole);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = "Bạn cần đăng nhập để thực hiện hành động này.",
                        needAuth = true
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                // Trường hợp request thường thì redirect về trang login
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Index" },
                        { "act", Constant.OPEN_LOGIN },
                        { "area", Constant.ROLE_USER }
                    });
            }
        }
    }
}