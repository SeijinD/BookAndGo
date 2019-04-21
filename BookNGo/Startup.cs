using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookNGo.Startup))]
namespace BookNGo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
