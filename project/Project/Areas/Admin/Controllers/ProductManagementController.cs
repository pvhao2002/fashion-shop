using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Project.Areas.Admin.DTO;
using Project.Areas.Admin.Payload;
using Project.Areas.Admin.ViewModel;
using Project.Entity;
using Project.Utils;

namespace Project.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class ProductManagementController : System.Web.Mvc.Controller
    {
        public async Task<ActionResult> Create()
        {
            using (var ctx = new DBConnection())
            {
                var categories = await ctx.categories
                    .Where(it => Constant.ACTIVE.Equals(it.status))
                    .Select(it => new SelectListItem
                    {
                        Value = it.category_id.ToString(),
                        Text = it.category_name
                    })
                    .ToListAsync();
                var model = new ProductViewModel
                {
                    Categories = categories,
                    ProductModel = new product()
                };
                return View(model);
            }
        }

        public async Task<ActionResult> Index(string status = "all", string search = "")
        {
            using (var ctx = new DBConnection())
            {
                search = search.ToLower();
                var products = (await ctx.products
                        .Where(item => "all".Equals(status) || item.status.Equals(status))
                        .Where(item => "".Equals(search) || item.name.ToLower().Contains(search.ToLower()))
                        .OrderBy(a => a.product_id)
                        .ToListAsync())
                    .Select(it => new ProductDTO(it))
                    .ToList();
                var categories = await ctx.categories
                    .Where(it => Constant.ACTIVE.Equals(it.status))
                    .Select(it => new SelectListItem
                    {
                        Value = it.category_id.ToString(),
                        Text = it.category_name
                    })
                    .ToListAsync();
                var model = new ProductViewModel
                {
                    Title = "Quản lý sản phẩm",
                    Products = products,
                    SearchTerm = search,
                    Categories = categories,
                    Status = status
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateImage(ProductDTO model, IEnumerable<HttpPostedFileBase> Images)
        {
            using (var ctx = new DBConnection())
            {
                var existingProduct = await ctx.products.FirstOrDefaultAsync(it => model.ProductId == it.product_id);
                if (existingProduct == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }

                if (Images != null)
                {
                    var existingImages = ctx.product_images.Where(it => it.product_id == existingProduct.product_id);
                    ctx.product_images.RemoveRange(existingImages);
                    await ctx.SaveChangesAsync();
                    foreach (var file in Images)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.InputStream.CopyToAsync(ms);
                                var fileBytes = ms.ToArray();
                                string base64String = Convert.ToBase64String(fileBytes);

                                ctx.product_images.Add(new product_images
                                {
                                    product_id = existingProduct.product_id,
                                    image_url = "data:" + file.ContentType + ";base64," + base64String
                                });
                            }
                        }
                    }
                    await ctx.SaveChangesAsync();
                }
                return Json(new { success = true, message = "Cập nhật hình ảnh sản phẩm thành công" });
            }
        }


        [HttpPost]
        public async Task<JsonResult> Update(ProductDTO model)
        {
            using (var ctx = new DBConnection())
            {
                if (string.IsNullOrEmpty(model.Name) || model.Price == null)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
                }

                var existingProduct = await ctx.products.FirstOrDefaultAsync(it => model.ProductId == it.product_id);
                if (existingProduct == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại" });
                }

                existingProduct.category_id = model.CategoryId;
                existingProduct.name = model.Name;
                existingProduct.description = model.Description;
                existingProduct.price = model.Price;
                existingProduct.stock = model.Stock;
                existingProduct.status = model.Status;
                existingProduct.updated_at = DateTime.Now;

                await ctx.SaveChangesAsync();
                return Json(new { success = true, message = "Cập nhật sản phẩm thành công" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Insert(ProductPayload model, IEnumerable<HttpPostedFileBase> Images)
        {
            if (string.IsNullOrEmpty(model.Name) || model.Price == null)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            using (var ctx = new DBConnection())
            {
                var newProduct = new product
                {
                    category_id = model.CategoryId,
                    name = model.Name,
                    description = model.Description,
                    price = model.Price,
                    stock = model.Stock,
                    status = Constant.ACTIVE,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };

                var returnProduct = ctx.products.Add(newProduct);
                await ctx.SaveChangesAsync();
                if (Images != null)
                {
                    foreach (var file in Images)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.InputStream.CopyToAsync(ms);
                                var fileBytes = ms.ToArray();
                                string base64String = Convert.ToBase64String(fileBytes);

                                ctx.product_images.Add(new product_images
                                {
                                    product_id = returnProduct.product_id,
                                    image_url = "data:" + file.ContentType + ";base64," + base64String
                                });
                            }
                        }
                    }

                    await ctx.SaveChangesAsync();
                }

                return Json(new { success = true, message = "Thêm sản phẩm thành công" });
            }
        }
    }
}