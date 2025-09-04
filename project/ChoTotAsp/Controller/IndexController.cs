using System.Web.Mvc;

namespace ChoTotAsp.Controller
{
    public class IndexController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            // Redirect to the Index action of HomeController
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
    }
}
