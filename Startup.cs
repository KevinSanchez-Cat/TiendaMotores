using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaMotores.Startup))]
namespace TiendaMotores
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
