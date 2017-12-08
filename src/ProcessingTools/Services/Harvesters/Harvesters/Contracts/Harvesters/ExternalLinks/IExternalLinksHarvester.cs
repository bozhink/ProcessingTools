namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Services.Harvesters;
    using ProcessingTools.Harvesters.Contracts.Models.ExternalLinks;

    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
