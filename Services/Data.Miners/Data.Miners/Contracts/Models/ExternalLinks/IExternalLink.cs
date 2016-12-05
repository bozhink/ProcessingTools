using ProcessingTools.Nlm.Publishing.Types;

namespace ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks
{
    public interface IExternalLink
    {
        string Content { get; }

        string Href { get; }

        ExternalLinkType Type { get; }
    }
}
