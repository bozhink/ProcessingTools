namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Harvesters.Contracts.Models.Abbreviations;
    using ProcessingTools.Services.Contracts.Harvesters;

    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
