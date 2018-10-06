using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignmnet4.Startup))]
namespace Assignmnet4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
