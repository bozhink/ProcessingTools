namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Harvesters.Contracts.Models.ExternalLinks;

    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
