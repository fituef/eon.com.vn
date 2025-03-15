using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EONBussiness.Startup))]
namespace EONBussiness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
