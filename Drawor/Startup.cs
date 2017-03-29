using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drawor.Startup))]
namespace Drawor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
