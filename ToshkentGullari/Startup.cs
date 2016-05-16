using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToshkentGullari.Startup))]
namespace ToshkentGullari
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
