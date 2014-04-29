using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VLV2014Test.Startup))]
namespace VLV2014Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
