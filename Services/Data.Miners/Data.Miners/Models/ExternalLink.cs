namespace ProcessingTools.Data.Miners.Models
{
    using Contracts.Models;
    using Nlm.Publishing.Types;

    public class ExternalLink : IExternalLink
    {
        public string Content { get; set; }

        public string Href { get; set; }

        public ExternalLinkType Type { get; set; }
    }
}
