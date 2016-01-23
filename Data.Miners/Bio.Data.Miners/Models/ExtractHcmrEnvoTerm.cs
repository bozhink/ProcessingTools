namespace ProcessingTools.Bio.Data.Miners.Models
{
    using Contracts;

    public class ExtractHcmrEnvoTerm : IExtractHcmrEnvoTerm
    {
        public string Content { get; set; }

        public int[] Types { get; set; }

        public string[] Identifiers { get; set; }
    }
}