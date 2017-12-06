namespace ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations
{
    using ProcessingTools.Harvesters.Contracts.Models.Abbreviations;
    using ProcessingTools.Contracts.Services.Harvesters;

    public interface IAbbreviationsHarvester : IEnumerableXmlHarvester<IAbbreviationModel>
    {
    }
}
