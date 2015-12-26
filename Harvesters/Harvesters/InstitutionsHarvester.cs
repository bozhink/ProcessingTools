namespace ProcessingTools.Harvesters
{
    using Contracts;
    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models.Contracts;

    public class InstitutionsHarvester : SimpleServiceStringHarvesterFactory<IInstitutionsDataService, IInstitution>, IInstitutionsHarvester
    {
        public InstitutionsHarvester(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}