namespace ProcessingTools.Data.Miners.Contracts.Miners.ExternalLinks
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks;

    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
