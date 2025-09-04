//using System;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using ChoTotAsp.Areas.User.Payload;
//using ChoTotAsp.Areas.User.ViewModel;
//using ChoTotAsp.Entity;
//using ChoTotAsp.Utils;

//namespace ChoTotAsp.Areas.User.Controllers
//{
//    [CustomAuthenticationFilter]
//    [CustomAuthorize(Constant.ROLE_USER)]
//    public class PostController : System.Web.Mvc.Controller
//    {
//        // GET
//        public async Task<ActionResult> Index(int? id)
//        {
//            var listProvinces = await DBContext.Instance.Provinces.ToListAsync();
//            var listCategory = await DBContext.Instance.Categories
//                .Where(item => Constant.ACTIVE.Equals(item.Status))
//                .ToListAsync();
//            var post = await DBContext.Instance.Products
//                .Include(item => item.Images)
//                .Include(item => item.Province)
//                .Include(item => item.Category)
//                .FirstOrDefaultAsync(item => item.ProductId == id);

//            var model = new PostViewModel()
//            {
//                DropdownProvinces = listProvinces
//                    .Select(item => new SelectListItem()
//                    {
//                        Text = item.ProvinceName,
//                        Value = item.ProvinceId.ToString()
//                    }).ToList(),
//                DropdownCategory = listCategory
//                    .Select(item => new SelectListItem()
//                    {
//                        Text = item.CategoryName,
//                        Value = item.CategoryId.ToString()
//                    }).ToList(),
//                Post = post ?? new Product(),
//            };
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<JsonResult> Create(PostRequest request)
//        {
//            var postItem = await DBContext.Instance.Products
//                .Include(item => item.Images)
//                .FirstOrDefaultAsync(item => item.ProductId == request.ProductId);
//            var mess = string.Empty;
//            if (postItem != null)
//            {
//                postItem.Title = request.Title;
//                postItem.Description = request.Description;
//                postItem.PhoneNumber = request.PhoneNumber;
//                postItem.Price = request.Price;
//                postItem.CategoryId = request.CategoryId;
//                postItem.ProvinceId = request.ProvinceId;
//                postItem.LastUpdatedDate = DateTime.Now;
//                postItem.LastUpdatedBy = Constant.ROLE_USER;
//                postItem.Status = Constant.PENDING;
//                // remove all images
//                DBContext.Instance.Images.RemoveRange(postItem.Images);
//                postItem.Images = request.ImageUrls.Select(item => new Image()
//                {
//                    // remove double quotes from the image url
//                    ImageUrl = item.Replace("\"", ""),
//                    Product = postItem,
//                    CreatedDate = DateTime.Now,
//                    LastUpdatedDate = DateTime.Now,
//                    CreatedBy = Constant.ROLE_USER,
//                    LastUpdatedBy = Constant.ROLE_USER
//                }).ToList();
//                mess = "Cập nhật bài post thành công";
//            }
//            else
//            {
//                var post = new Product()
//                {
//                    Title = request.Title,
//                    Description = request.Description,
//                    PhoneNumber = request.PhoneNumber,
//                    Price = request.Price,
//                    CategoryId = request.CategoryId,
//                    ProvinceId = request.ProvinceId,
//                    AccountId = AuthenticationUtil.GetUserId(Request, Session),
//                    Status = Constant.PENDING,
//                    CreatedDate = DateTime.Now,
//                    LastUpdatedDate = DateTime.Now,
//                    CreatedBy = Constant.ROLE_USER,
//                    LastUpdatedBy = Constant.ROLE_USER
//                };
//                post.Images = request.ImageUrls.Select(item => new Image()
//                {
//                    // remove double quotes from the image url
//                    ImageUrl = item.Replace("\"", ""),
//                    Product = post,
//                    CreatedDate = DateTime.Now,
//                    LastUpdatedDate = DateTime.Now,
//                    CreatedBy = Constant.ROLE_USER,
//                    LastUpdatedBy = Constant.ROLE_USER
//                }).ToList();
//                DBContext.Instance.Products.Add(post);
//                mess = "Đăng bài post thành công";
//            }

//            await DBContext.Instance.SaveChangesAsync();
//            return Json(new { success = true, message = mess });
//        }
//    }
//}