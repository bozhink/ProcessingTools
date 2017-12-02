namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Harvesters.Contracts.Models.ExternalLinks;
    using ProcessingTools.Services.Contracts.Harvesters;

    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
