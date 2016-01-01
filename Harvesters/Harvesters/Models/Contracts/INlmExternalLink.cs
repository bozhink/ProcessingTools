namespace ProcessingTools.Harvesters.Models.Contracts
{
    using Nlm.Publishing.Types;

    public interface INlmExternalLink
    {
        string Content { get; set; }

        ExternalLinkType Type { get; set; }
    }
}
