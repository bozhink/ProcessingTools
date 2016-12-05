namespace ProcessingTools.Data.Miners.Models.Bio.Environments
{
    using ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments;

    public class ExtractHcmrEnvoTerm : IExtractHcmrEnvoTerm
    {
        public string Content { get; set; }

        public int[] Types { get; set; }

        public string[] Identifiers { get; set; }
    }
}
