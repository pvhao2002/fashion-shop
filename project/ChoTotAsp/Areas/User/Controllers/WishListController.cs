//using System;
//using System.Data.Entity;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using ChoTotAsp.Entity;
//using ChoTotAsp.Utils;

//namespace ChoTotAsp.Areas.User.Controllers
//{
//    [CustomAuthenticationFilter]
//    [CustomAuthorize(Constant.ROLE_USER)]
//    public class WishListController : System.Web.Mvc.Controller
//    {
//        [HttpPost]
//        public async Task<JsonResult> Add(int id)
//        {
//            var userId = AuthenticationUtil.GetUserId(Request, Session);
//            var user = await DBContext.Instance.Accounts
//                .FirstOrDefaultAsync(a => a.AccountId == userId);

//            if (user == null)
//            {
//                return Json(new { success = false, message = "Người dùng không tồn tại", type = "danger" });
//            }

//            var product = await DBContext.Instance.Products
//                .FirstOrDefaultAsync(p => p.ProductId == id);
//            if (product == null)
//            {
//                return Json(new { success = false, message = "Sản phẩm không tồn tại", type = "danger" });
//            }

//            var wishList = await DBContext.Instance.WishLists
//                .FirstOrDefaultAsync(w => w.AccountId == userId && w.ProductId == id);

//            if (wishList != null)
//            {
//                return Json(new
//                    { success = false, message = "Sản phẩm đã có trong danh sách yêu thích", type = "warning" });
//            }

//            DBContext.Instance.WishLists.Add(new WishList
//            {
//                AccountId = userId,
//                ProductId = id,
//                CreatedBy = user.Username,
//                CreatedDate = DateTime.Now,
//                LastUpdatedBy = user.Username,
//                LastUpdatedDate = DateTime.Now,
//                Status = Constant.ACTIVE
//            });

//            await DBContext.Instance.SaveChangesAsync();

//            return Json(new { success = true, message = "Thêm vào danh sách yêu thích thành công", type = "success" });
//        }

//        [HttpPost]
//        public async Task<JsonResult> Remove(int id)
//        {
//            var userId = AuthenticationUtil.GetUserId(Request, Session);
//            var user = await DBContext.Instance.Accounts
//                .FirstOrDefaultAsync(a => a.AccountId == userId);

//            if (user == null)
//            {
//                return Json(new { success = false, message = "Người dùng không tồn tại", type = "danger" });
//            }

//            var product = await DBContext.Instance.Products
//                .FirstOrDefaultAsync(p => p.ProductId == id);
//            if (product == null)
//            {
//                return Json(new { success = false, message = "Sản phẩm không tồn tại", type = "danger" });
//            }

//            var wishList = await DBContext.Instance.WishLists
//                .FirstOrDefaultAsync(w => w.AccountId == userId && w.ProductId == id);

//            if (wishList == null)
//            {
//                return Json(new
//                    { success = false, message = "Sản phẩm chưa có có trong danh sách yêu thích", type = "warning" });
//            }
//            DBContext.Instance.WishLists.Remove(wishList);
//            await DBContext.Instance.SaveChangesAsync();
//            return Json(new { success = true, message = "Xóa khỏi danh sách yêu thích thành công", type = "success" });
//        }
//    }
//}