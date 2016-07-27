using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularMVCAuthentication.Startup))]
namespace AngularMVCAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
