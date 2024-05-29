using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APIInsideExistingMVCProject.Startup))]
namespace APIInsideExistingMVCProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
