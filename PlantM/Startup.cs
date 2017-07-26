using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlantM.Startup))]
namespace PlantM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
