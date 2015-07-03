using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SvNaum.Web.Startup))]
namespace SvNaum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
