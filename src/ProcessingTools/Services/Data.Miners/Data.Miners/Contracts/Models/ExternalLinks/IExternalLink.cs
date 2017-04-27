namespace ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks
{
    using ProcessingTools.Nlm.Publishing.Types;

    public interface IExternalLink
    {
        string Content { get; }

        string Href { get; }

        ExternalLinkType Type { get; }
    }
}
