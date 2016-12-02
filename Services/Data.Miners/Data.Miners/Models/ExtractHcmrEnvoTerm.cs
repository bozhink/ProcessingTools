namespace ProcessingTools.Data.Miners.Models
{
    using Contracts.Models;

    public class ExtractHcmrEnvoTerm : IExtractHcmrEnvoTerm
    {
        public string Content { get; set; }

        public int[] Types { get; set; }

        public string[] Identifiers { get; set; }
    }
}
