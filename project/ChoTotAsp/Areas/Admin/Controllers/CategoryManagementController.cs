using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<JsonResult> InsertOrUpdate(category model, HttpPostedFileBase imageFile)
        {
            JsonResult jsonResult;
            using (var ctx = new DBConnection())
            {
                if (string.IsNullOrEmpty(model.category_name) || string.IsNullOrEmpty(model.status))
                {
                    jsonResult = Json(
                        new { success = false, message = "Dữ liệu không hợp lệ" },
                        JsonRequestBehavior.AllowGet
                    );
                    return await Task.FromResult(jsonResult);
                }

                // Nếu có upload ảnh thì convert sang base64
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await imageFile.InputStream.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        model.image = Convert.ToBase64String(fileBytes);
                    }
                }

                try
                {
                    if (model.category_id > 0) // Update
                    {
                        var existingCategory = await ctx.categories.FindAsync(model.category_id);
                        if (existingCategory == null)
                        {
                            jsonResult = Json(
                                new { success = false, message = "Danh mục không tồn tại" },
                                JsonRequestBehavior.AllowGet
                            );
                            return await Task.FromResult(jsonResult);
                        }

                        existingCategory.category_name = model.category_name;
                        existingCategory.updated_at = DateTime.Now;

                        if (!string.IsNullOrEmpty(model.image))
                        {
                            existingCategory.image = model.image; // chỉ update ảnh khi có file mới
                        }
                    }
                    else // Insert
                    {
                        var newCategory = new category
                        {
                            category_name = model.category_name,
                            status = model.status,
                            image = model.image,
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
                catch (DbEntityValidationException ex)
                {
                    jsonResult = Json(
                        new { success = false, message = ex.Message },
                        JsonRequestBehavior.AllowGet
                    );
                }
            }

            return await Task.FromResult(jsonResult);
        }


        public ActionResult Create()
        {
            return View(new category());
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
            using (var ctx = new DBConnection())
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
            using (var ctx = new DBConnection())
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