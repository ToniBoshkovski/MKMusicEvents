using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MKMusicEvents.Startup))]
namespace MKMusicEvents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
