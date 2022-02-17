using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test01.Startup))]
namespace test01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
