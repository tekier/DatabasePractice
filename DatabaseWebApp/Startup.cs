using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatabaseWebApp.Startup))]
namespace DatabaseWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
