namespace ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks
{
    using ProcessingTools.Enumerations.Nlm;

    public interface IExternalLink
    {
        string Content { get; }

        string Href { get; }

        ExternalLinkType Type { get; }
    }
}
