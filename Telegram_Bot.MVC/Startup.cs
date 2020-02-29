using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(studentassistant_bot.Startup))]
namespace studentassistant_bot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
