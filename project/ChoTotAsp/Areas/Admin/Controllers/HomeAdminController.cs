using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChoTotAsp.Areas.Admin.ViewModel;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class HomeAdminController : System.Web.Mvc.Controller
    {
        public async Task<ActionResult> Index()
        {
            //var totalUsers = await DBContext.Instance.Accounts.CountAsync(item => Constant.ROLE_USER.Equals(item.Role));
            //var totalMessages = await DBContext.Instance.Messages.CountAsync();
            //var totalProducts = await DBContext.Instance.Products.CountAsync();
            //var totalCategories = await DBContext.Instance.Categories.CountAsync();
            //var totalProductPending =
            //    await DBContext.Instance.Products.CountAsync(item => Constant.PENDING.Equals(item.Status));
            //var totalProductApproved =
            //    await DBContext.Instance.Products.CountAsync(item => Constant.APPROVED.Equals(item.Status));

            var model = new DashboardViewModel
            {
                Title = "Trang chủ",
                TotalUsers = 0,
                TotalMessages = 0,
                TotalProducts = 0,
                TotalCategories = 0,
                TotalProductPending = 0,
                TotalProductApproved = 0
            };

            return View(model);
        }
    }
}