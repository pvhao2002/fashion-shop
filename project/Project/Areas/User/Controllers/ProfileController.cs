using System.Threading.Tasks;
using System.Web.Mvc;
using Project.Areas.User.ViewModel;
using Project.Entity;
using Project.Utils;

namespace Project.Areas.User.Controllers
{
    [CustomAuthenticationFilter]
    [CustomAuthorize(Constant.ROLE_USER)]
    public class ProfileController : System.Web.Mvc.Controller
    {
        // GET
        public async Task<ActionResult> Index()
        {
            return View(new ProfileViewModel());
        }

        public async Task<ActionResult> View(int id)
        {
            using(var ctx = new DBConnection())
            {
                var model = new ProfileViewModel()
                {
                };
                return View(model);
            }
        }
    }
}