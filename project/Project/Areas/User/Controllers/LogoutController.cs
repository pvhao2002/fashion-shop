using System;
using System.Web.Mvc;

namespace Project.Areas.User.Controllers
{
    public class LogoutController : System.Web.Mvc.Controller
    {
        // GET
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