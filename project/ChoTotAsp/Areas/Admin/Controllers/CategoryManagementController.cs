using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChoTotAsp.Areas.Admin.ViewModel;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class CategoryManagementController : System.Web.Mvc.Controller
    {
        // GET: Admin/CategoryManagement
        public async Task<ActionResult> Index(string searchTerm = "")
        {
            using(var ctx = new DBConnection())
            {
                searchTerm = searchTerm.ToLower();
                var categories = await ctx.categories
                    .Where(item => item.category_name.ToLower().Contains(searchTerm))
                    .OrderBy(a => a.category_id)
                    .ToListAsync();

                var model = new CategoryViewModel()
                {
                    Title = "Quản lý danh mục sản phẩm",
                    Categories = categories,
                    SearchTerm = searchTerm
                };

                return View(model);
            }
        }

        public async Task<JsonResult> GetCategoryById(int categoryId)
        {
            using(var ctx = new DBConnection())
            {
                var category = await ctx.categories.FirstOrDefaultAsync(a => a.category_id == categoryId);

                if (category == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy danh mục sản phẩm" },
                        JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        categoryId = category.category_id,
                        categoryName = category.category_name,
                        Status = category.status,
                    }
                },
                    JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditCategory(int categoryId, string categoryName, string categoryImage,
            string status)
        {

            using(var ctx = new DBConnection())
            {
                if (string.IsNullOrEmpty(status) || string.IsNullOrEmpty(categoryName) ||
                string.IsNullOrEmpty(categoryImage))
                {
                    return Json(new { success = false, message = "Invalid Category data" });
                }

                var category = await ctx.categories.FirstOrDefaultAsync(a => a.category_id == categoryId);

                if (category == null)
                {
                    return Json(new { success = false, message = "Category not found" });
                }

                category.status = status;
                category.category_name = categoryName;
                try
                {
                    await ctx.SaveChangesAsync();
                    return Json(new { success = true, message = "Category updated successfully" });
                }
                catch (DbEntityValidationException ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddCategory(string categoryName, string categoryImage, string status)
        {
            using( var ctx = new DBConnection())
            {
                var category = new category()
                {
                    category_name = categoryName,
                    status = status,
                    created_at = DateTime.Now,
                };
                ctx.categories.Add(category);
                await ctx.SaveChangesAsync();
                return Json(new { success = true, message = "Category added successfully" });
            }
        }
    }
}