using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HebrewLanguageLearning_Admin.Startup))]
namespace HebrewLanguageLearning_Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
