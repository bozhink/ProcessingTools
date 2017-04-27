namespace ProcessingTools.Harvesters.Contracts.Harvesters.ExternalLinks
{
    using Models.ExternalLinks;
    using ProcessingTools.Contracts.Harvesters;

    public interface IExternalLinksHarvester : IGenericEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}