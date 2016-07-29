using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularMVCCustomAuthentication.Startup))]
namespace AngularMVCCustomAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
