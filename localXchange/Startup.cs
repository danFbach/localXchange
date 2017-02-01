using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(localXchange.Startup))]
namespace localXchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
