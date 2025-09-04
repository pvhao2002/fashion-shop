using System.Web.Mvc;

namespace ChoTotAsp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "HomeAdmin", action = "Index", id = UrlParameter.Optional },
                new[] { "ChoTotAsp.Areas.Admin.Controllers" }
            );
        }
    }
}