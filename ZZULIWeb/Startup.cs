using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZZULIWeb.Startup))]
namespace ZZULIWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
