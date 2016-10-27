namespace ProcessingTools.Data.Miners.Models
{
    using Nlm.Publishing.Types;

    public class NlmExternalLink
    {
        public string Content { get; set; }

        public string Href { get; set; }

        public ExternalLinkType Type { get; set; }
    }
}