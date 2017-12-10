namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Models.Harvesters.Abbreviations;

    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
