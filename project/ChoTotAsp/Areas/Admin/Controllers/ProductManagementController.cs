using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;
using ChoTotAsp.Areas.Admin.ViewModel;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class ProductManagementController : System.Web.Mvc.Controller
    {
        // GET: Admin/ProductManagement
        public async Task<ActionResult> Index(string status = "pending", string search = "")
        {
            using (var ctx = new DBConnection())
            {
                search = search.ToLower();
                var products = await ctx.products
                    .Where(item => "all".Equals(status) || item.status.Equals(status))
                    .Where(item => "".Equals(search) || item.name.ToLower().Contains(search.ToLower()))
                    .OrderBy(a => a.product_id)
                    .ToListAsync();

                var model = new ProductViewModel
                {
                    Title = "Quản lý sản phẩm",
                    Products = products,
                    SearchTerm = search,
                    Status = status
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(int productId, string status)
        {
            using (var ctx = new DBConnection())
            {
                var product = await ctx.products.FirstOrDefaultAsync(a => a.product_id == productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
                }

                product.status = status;
                await ctx.SaveChangesAsync();

                return Json(new { success = true, message = "Product updated successfully" });
            }
        }
    }
}