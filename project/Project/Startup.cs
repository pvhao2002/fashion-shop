using Project;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))] // Thêm dòng này để đánh dấu OWIN startup
namespace Project
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