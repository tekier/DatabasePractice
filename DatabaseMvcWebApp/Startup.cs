using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatabaseMvcWebApp.Startup))]
namespace DatabaseMvcWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
