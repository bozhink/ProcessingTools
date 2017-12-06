namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Harvesters.Contracts.Models.ExternalLinks;
    using ProcessingTools.Contracts.Services.Harvesters;

    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
