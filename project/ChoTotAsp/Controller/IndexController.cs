using System.Web.Mvc;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Controller
{
    public class IndexController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            // Redirect to the Index action of HomeController
            return RedirectToAction("Index", "Home",
                new
                {
                    area = AuthenticationUtil.getCurrentUser(Request, Session) ??
                           new user { role = Constant.ROLE_USER }
                });
        }
    }
}