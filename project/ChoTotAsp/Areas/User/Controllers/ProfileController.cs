using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChoTotAsp.Areas.User.Payload;
using ChoTotAsp.Areas.User.ViewModel;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.User.Controllers
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
                var posts = await ctx.products
                                .Include(p => p.product_images)
                                .Include(p => p.category)
                                .Where(p => Constant.APPROVED.Equals(p.status))
                                .ToListAsync();
                var vendor = await ctx.users
                    .FirstOrDefaultAsync(a => a.user_id == id);
                var model = new ProfileViewModel()
                {
                };
                return View(model);
            }
        }
    }
}