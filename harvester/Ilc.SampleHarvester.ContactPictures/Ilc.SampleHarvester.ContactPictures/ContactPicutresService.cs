using Ilc.InformationHarvester;
using Ilc.SampleHarvester.ContactPictures.DataCube;
using Ilc.WcfService;

namespace Ilc.SampleHarvester.ContactPictures
{
    [FaultServiceBehavior]
    public class ContactPicutresService : HarvesterService
    {
        public ContactPicutresService()
            : base(new ContactPicutresDataCube())
        {
        }
    }
}