using System;
using System.Web.Mvc;
using ChoTotAsp.Utils;

namespace ChoTotAsp.Areas.Admin.Controllers
{
    public class LogoutAdminController: System.Web.Mvc.Controller
    {
        // GET: Admin/LogoutAdmin
        public ActionResult Index()
        {
            Session.Clear();
            foreach (var cookie in Request.Cookies.AllKeys)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index", "Home", new { area = "User" });
        }
    }
}
