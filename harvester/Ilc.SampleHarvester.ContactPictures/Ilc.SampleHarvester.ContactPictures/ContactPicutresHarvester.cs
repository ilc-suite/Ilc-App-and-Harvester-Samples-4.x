using Ilc.SampleHarvester.ContactPictures.DataCube;
using Ilc.InformationHarvester;
using Ilc.WcfService;

namespace Ilc.SampleHarvester.ContactPictures
{
    [OneWayFaultServiceBehavior]
    public class ContactPicutresHarvester : Ilc.InformationHarvester.InformationHarvester
    {
        public ContactPicutresHarvester()
            : base(new ContactPicutresDataCube())
        {
        }
    }
}