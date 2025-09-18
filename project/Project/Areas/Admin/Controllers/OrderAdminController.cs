using System.Web.Mvc;
using Project.Utils;

namespace Project.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class OrderAdminController : System.Web.Mvc.Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}