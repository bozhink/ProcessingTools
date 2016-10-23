namespace ProcessingTools.Harvesters.Contracts.Abbreviations
{
    using Models.Abbreviations;

    using ProcessingTools.Contracts.Harvesters;

    public interface IAbbreviationsHarvester : IGenericQueryableXmlHarvester<IAbbreviationModel>
    {
    }
}
