using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChoTotAsp.Areas.Admin.ViewModel;
using ChoTotAsp.Entity;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.Admin.Controllers
{
    [CustomAuthorize(Constant.ROLE_ADMIN)]
    public class UserManagementController : System.Web.Mvc.Controller
    {
        // GET: Admin/UserManagement
        public async Task<ActionResult> Index(int page = 1, string searchTerm = "")
        {
            //const int pageSize = 10;
            //searchTerm = searchTerm.Trim().ToLower();
            //var totalUsers = await
            //    DBContext.Instance.Accounts.CountAsync(a =>
            //        Constant.ROLE_USER.Equals(a.Role) &&
            //        (string.Empty.Equals(searchTerm) || a.Username.ToLower().Contains(searchTerm)));
            //var totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            //var accounts = await DBContext.Instance.Accounts
            //    .Where(a => Constant.ROLE_USER.Equals(a.Role) &&
            //                (string.Empty.Equals(searchTerm) || a.Username.ToLower().Contains(searchTerm)))
            //    .OrderBy(a => a.AccountId)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();

            var model = new UserManagementViewModel
            {
                Title = "Quản lý người dùng",
                Accounts = new List<user>(),
                CurrentPage = page,
                TotalPages = 0,
                SearchTerm = searchTerm
            };

            return View(model);
        }


        public async Task<ActionResult> Statistic()
        {
            //var listUser = await DBContext.Instance.Accounts
            //    .Include(item => item.Messages)
            //    .Include(item => item.Products)
            //    .Select(item => new UserStatistic()
            //    {
            //        Email = item.Email,
            //        Username = item.Username,
            //        Status = item.Status,
            //        TotalMessages = item.Messages.Count,
            //        TotalProducts = item.Products.Count
            //    })
            //    .ToListAsync();
            var model = new UserManagementViewModel()
            {
                Title = "Thống kê người dùng",
                ListUserStatistic = new List<UserStatistic>()
            };
            return View(model);
        }

        // GET: Admin/UserManagement/GetUserById
        public ActionResult GetUserById(int accountId)
        {
            //var user = DBContext.Instance.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            //if (user == null)
            //{
            //    return Json(new { success = false, message = "User not found" }, JsonRequestBehavior.AllowGet);
            //}

            return Json(new
                {
                    success = true,
                    data = new
                    {
                    }
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditUser(int accountId, string userName, string status)
        {
            //if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(status))
            //{
            //    return Json(new { success = false, message = "Invalid user data" });
            //}

            //var user = DBContext.Instance.Accounts.SingleOrDefault(a => a.AccountId == accountId);

            //user.Status = status;
            //user.Username = userName;

            //DBContext.Instance.SaveChanges();

            return Json(new { success = true, message = "User updated successfully" });
        }
    }
}