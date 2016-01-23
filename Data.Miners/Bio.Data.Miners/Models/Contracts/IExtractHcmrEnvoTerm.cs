namespace ProcessingTools.Bio.Data.Miners.Models.Contracts
{
    public interface IExtractHcmrEnvoTerm
    {
        string Content { get; set; }

        int[] Types { get; set; }

        string[] Identifiers { get; set; }
    }
}