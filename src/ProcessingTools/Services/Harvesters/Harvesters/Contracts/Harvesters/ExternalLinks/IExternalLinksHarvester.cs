namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Models.Harvesters.ExternalLinks;

    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
