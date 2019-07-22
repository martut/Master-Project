using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoookingAIOProject.MVC.Startup))]
namespace BoookingAIOProject.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
