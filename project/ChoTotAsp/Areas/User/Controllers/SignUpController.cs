using ChoTotAsp.Entity;
using ChoTotAsp.Utils;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChoTotAsp.Areas.User.Controllers
{
    public class SignUpController : System.Web.Mvc.Controller
    {
        [HttpPost]
        public async Task<JsonResult> Index(string email, string password, string fullname)
        {
            using(var ctx = new DBConnection())
            {
                var user = await ctx.users.FirstOrDefaultAsync(item => item.email.Equals(email));
                if (user != null)
                {
                    return Json(new
                    {
                        success = false,
                        message = Constant.INACTIVE.Equals(user.status)
                            ? "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ với quản trị viên"
                            : "Email đã tồn tại"
                    });
                }

                var account = new user
                {
                    email = email,
                    password_hash = password,
                    full_name = fullname,
                    status = Constant.ACTIVE,
                    role = Constant.ROLE_USER,
                    created_at = System.DateTime.Now,
                    avatar = Constant.DEFAULT_AVATAR,
                };

                ctx.users.Add(account);
                await ctx.SaveChangesAsync();

                return Json(new { success = true, message = "Đăng ký thành công" });
            }
        }
    }
}