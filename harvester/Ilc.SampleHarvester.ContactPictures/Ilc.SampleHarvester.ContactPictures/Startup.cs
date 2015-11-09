using Ilc.InformationHarvester;
using Ilc.SampleHarvester.ContactPictures;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Ilc.SampleHarvester.ContactPictures
{
    public class Startup : HarvesterStartupBase
    {
        public void Configuration(IAppBuilder app)
        {
            Initialize();
        }
    }
}