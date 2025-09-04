using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.WsFederation;
using Owin;

namespace ChoTotAsp
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(WsFederationAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = WsFederationAuthenticationDefaults.AuthenticationType
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "997318831095-ebepk2ev2rorbnpl7rov4prm0vaogmgu.apps.googleusercontent.com",
                ClientSecret = "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",
                CallbackPath = new PathString("/Admin/SigninGoogle")
            });
        }
    }
}