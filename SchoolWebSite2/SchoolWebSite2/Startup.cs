using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolWebSite2.Startup))]
namespace SchoolWebSite2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
