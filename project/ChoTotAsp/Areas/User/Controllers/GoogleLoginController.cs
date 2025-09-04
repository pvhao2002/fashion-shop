using System.Web.Mvc;
using ChoTotAsp.Areas.Admin.Controllers;

namespace ChoTotAsp.Areas.User.Controllers
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