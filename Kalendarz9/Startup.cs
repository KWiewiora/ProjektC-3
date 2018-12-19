using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kalendarz9.Startup))]
namespace Kalendarz9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
