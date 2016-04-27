using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LengthOnPropertyValue.Startup))]
namespace LengthOnPropertyValue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
