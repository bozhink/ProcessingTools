namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Biorepositories;

    public interface IBiorepositoriesCollectionsDataMiner : IDataMiner<string, ICollection>
    {
    }
}
