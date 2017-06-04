namespace ProcessingTools.Data.Miners.Models.ExternalLinks
{
    using ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks;
    using ProcessingTools.Enumerations.Nlm;

    public class ExternalLink : IExternalLink
    {
        public string Content { get; set; }

        public string Href { get; set; }

        public ExternalLinkType Type { get; set; }
    }
}
