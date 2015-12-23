namespace ProcessingTools.Bio.Harvesters.Models
{
    using Contracts;

    public class ExtractHcmrEnvoTerm : IExtractHcmrEnvoTerm
    {
        public string Content { get; set; }

        public int[] Types { get; set; }

        public string[] Identifiers { get; set; }
    }
}