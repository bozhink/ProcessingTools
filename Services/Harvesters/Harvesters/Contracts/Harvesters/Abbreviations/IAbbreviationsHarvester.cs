namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using Models.Abbreviations;
    using ProcessingTools.Contracts.Harvesters;

    public interface IAbbreviationsHarvester : IGenericEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
