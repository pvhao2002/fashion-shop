using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Project.Entity;
using Project.Utils;

namespace Project.Areas.User.Controllers
{
    public class ForgetPasswordController : System.Web.Mvc.Controller
    {
        // GET
        [HttpPost]
        public async Task<JsonResult> Index(string email)
        {
            using(var ctx = new DBConnection())
            {
                var user = await ctx.users.FirstOrDefaultAsync(item => item.email.Equals(email));
                if (user == null)
                {
                    return Json(new { success = false, message = "Email không tồn tại" });
                }

                var newPassword = Guid.NewGuid().ToString().Substring(0, 8);
                var result = await MailUtils.SendEmail(user.email, "Quên mật khẩu",
                    "Mật khẩu mới của bạn là: " + newPassword);

                if (!Constant.SUCCESS_RS.Equals(result[Constant.STATUS_RS]))
                    return Json(new { success = false, Message = result[Constant.MESSAGE_RS] });

                user.password_hash = newPassword;

                await ctx.SaveChangesAsync();
                return Json(new { success = true, message = "Mật khẩu mới đã được gửi tới email của bạn" });
            }
        }
    }
}