using ChoTotAsp;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))] // Thêm dòng này để đánh dấu OWIN startup
namespace ChoTotAsp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}