namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Contracts.Services.Models.Data.Bio.Biorepositories;

    public interface IBiorepositoriesInstitutionsDataMiner : IDataMiner<string, IInstitution>
    {
    }
}
