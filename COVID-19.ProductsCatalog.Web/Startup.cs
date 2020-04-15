using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(COVID_19.ProductsCatalog.Web.Startup))]
namespace COVID_19.ProductsCatalog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
