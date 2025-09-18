using System.Web.Mvc;
using Project.Areas.Admin.Controllers;

namespace Project.Areas.User.Controllers
{
    public class GoogleLoginController : System.Web.Mvc.Controller
    {
        // GET
        public ActionResult Index()
        {
            return new ChallengeResult("Google", Url.Action("ExternalLoginCallback", "Home"));
        }
    }
}