using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Project.Areas.Admin.DTO;
using Project.Entity;
using Project.Utils;

namespace Project.Areas.User.Controllers
{
    public class ProductController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            using (var ctx = new DBConnection())
            {
                var categories = ctx.categories.Where(it => Constant.ACTIVE.Equals(it.status)).ToList();
                var products = ctx.products.Where(it => Constant.ACTIVE.Equals(it.status))
                    .ToList()
                    .Select(it => new ProductDTO(it))
                    .ToList();
                var model = new ViewModel.ProductViewModel
                {
                    ListCategory = categories,
                    Products = products,
                    TotalProduct = products.Count
                };
                return View(model);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            using (var ctx = new DBConnection())
            {
                var product = await ctx.products.FirstOrDefaultAsync(it => it.product_id == id);
                var relativeProducts = (await ctx.products
                        .Where(it => it.category_id == product.category_id && it.product_id != product.product_id)
                        .ToListAsync())
                    .Select(it => new ProductDTO(it))
                    .ToList();
                return View(new ViewModel.ProductViewModel
                {
                    Product = new ProductDTO(product),
                    RelativeProduct = relativeProducts
                });
            }
        }
    }
}