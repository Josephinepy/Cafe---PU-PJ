using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cafe.Startup))]
namespace Cafe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
