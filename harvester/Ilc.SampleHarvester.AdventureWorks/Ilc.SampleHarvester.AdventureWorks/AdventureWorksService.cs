using Ilc.InformationHarvester;
using Ilc.SampleHarvester.AdventureWorks.DataCube;
using Ilc.WcfService;

namespace Ilc.SampleHarvester.AdventureWorks
{
    [FaultServiceBehavior]
    public class AdventureWorksService : HarvesterService
    {
        public AdventureWorksService()
            : base(new AdventureWorksDataCube())
        {
        }
    }
}