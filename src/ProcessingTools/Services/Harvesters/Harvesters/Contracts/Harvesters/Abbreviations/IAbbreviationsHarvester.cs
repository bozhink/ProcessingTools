namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Contracts.Services.Harvesters;
    using ProcessingTools.Harvesters.Contracts.Models.Abbreviations;

    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
