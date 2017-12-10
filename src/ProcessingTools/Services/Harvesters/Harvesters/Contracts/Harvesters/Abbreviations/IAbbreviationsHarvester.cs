namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Harvesters.Contracts.Models.Abbreviations;

    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
