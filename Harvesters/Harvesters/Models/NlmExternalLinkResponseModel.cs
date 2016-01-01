namespace ProcessingTools.Harvesters.Models
{
    using Contracts;
    using Nlm.Publishing.Types;

    public class NlmExternalLinkResponseModel : INlmExternalLink
    {
        public string Content { get; set; }

        public ExternalLinkType Type { get; set; }
    }
}