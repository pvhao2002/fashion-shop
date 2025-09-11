using System.Web.Mvc;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Controller
{
    public class IndexController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var user = AuthenticationUtil.getCurrentUser(Request, Session) ?? new user { role = Constant.ROLE_USER };
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