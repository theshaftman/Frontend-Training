using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetAppl.Startup))]
namespace GetAppl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
