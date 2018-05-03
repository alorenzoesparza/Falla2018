using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Falla2018.Backend.Startup))]
namespace Falla2018.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
