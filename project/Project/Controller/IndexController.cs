using System.Web.Mvc;
using Project.Entity;
using Project.Utils;

namespace Project.Controller
{
    public class IndexController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var user = AuthenticationUtil.GetCurrentUser(Request, Session) ?? new user { role = Constant.ROLE_USER };
            var area = user.role;
            var controller = Constant.ROLE_ADMIN.Equals(area) ? "HomeAdmin" : "Home";
            // Redirect to the Index action of HomeController
            return RedirectToAction("Index", controller,
                new
                {
                    area
                });
        }
    }
}