namespace ProcessingTools.Data.Miners.Contracts.Miners.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.SpecimenCodes;

    public interface ISpecimenCodesByPatternDataMiner : IDataMiner<string, ISpecimenCode>
    {
    }
}
