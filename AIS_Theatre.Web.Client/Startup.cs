using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AIS_Theatre.Web.Client.Startup))]
namespace AIS_Theatre.Web.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
