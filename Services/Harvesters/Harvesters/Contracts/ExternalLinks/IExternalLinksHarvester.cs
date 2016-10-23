namespace ProcessingTools.Harvesters.Contracts.ExternalLinks
{
    using Models.ExternalLinks;

    using ProcessingTools.Contracts.Harvesters;

    public interface IExternalLinksHarvester : IGenericQueryableXmlHarvester<IExternalLinkModel>
    {
    }
}