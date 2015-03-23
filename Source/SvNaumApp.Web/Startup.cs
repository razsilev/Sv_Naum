using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SvNaumApp.Web.Startup))]
namespace SvNaumApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
