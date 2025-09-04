using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace ChoTotAsp.Areas.User.ViewModel
{
    public class ChallengeResult : HttpUnauthorizedResult
    {
        public ChallengeResult(string provider, string redirectUri)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }

        private string LoginProvider { get; set; }
        private string RedirectUri { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }
}