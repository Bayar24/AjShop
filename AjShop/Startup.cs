using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjShop.Startup))]
namespace AjShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
