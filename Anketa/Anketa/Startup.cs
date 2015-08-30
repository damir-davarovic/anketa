using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Anketa.Startup))]
namespace Anketa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
