using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChoTotAsp.Areas.Admin.Payload;
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
            using (var ctx = new DBConnection())
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

        [HttpPost]
        public async Task<JsonResult> InsertOrUpdate(CategoryPayload model)
        {
            JsonResult jsonResult;
            using (var ctx = new DBConnection())
            {
                if (string.IsNullOrEmpty(model.categoryName) || string.IsNullOrEmpty(model.status))
                {
                    jsonResult = Json(
                        new { success = false, message = "Dữ liệu không hợp lệ" },
                        JsonRequestBehavior.AllowGet
                    );
                    return await Task.FromResult(jsonResult);
                }

                try
                {
                    if (model.categoryId > 0) // Update
                    {
                        var existingCategory = await ctx.categories.FindAsync(model.categoryId);
                        if (existingCategory == null)
                        {
                            jsonResult = Json(
                                new { success = false, message = "Danh mục không tồn tại" },
                                JsonRequestBehavior.AllowGet
                            );
                            return await Task.FromResult(jsonResult);
                        }

                        existingCategory.category_name = model.categoryName;
                        existingCategory.updated_at = DateTime.Now;

                        if (!string.IsNullOrEmpty(model.categoryImage))
                        {
                            existingCategory.image = model.categoryImage; // chỉ update ảnh khi có file mới
                        }
                    }
                    else // Insert
                    {
                        var newCategory = new category
                        {
                            category_name = model.categoryName,
                            status = model.status,
                            image = model.categoryImage,
                            created_at = DateTime.Now,
                            updated_at = DateTime.Now
                        };

                        ctx.categories.Add(newCategory);
                    }

                    await ctx.SaveChangesAsync();

                    jsonResult = Json(
                        new { success = true, message = "Lưu danh mục sản phẩm thành công" },
                        JsonRequestBehavior.AllowGet
                    );
                }
                catch (Exception ex)
                {
                    jsonResult = Json(
                        new { success = false, message = ex.Message },
                        JsonRequestBehavior.AllowGet
                    );
                }
            }

            return await Task.FromResult(jsonResult);
        }

        public async Task<JsonResult> GetCategoryById(int categoryId)
        {
            using (var ctx = new DBConnection())
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
                            CategoryImage = category.image,
                            Status = category.status,
                        }
                    },
                    JsonRequestBehavior.AllowGet);
            }
        }
    }
}