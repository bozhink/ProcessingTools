namespace ProcessingTools.Bio.Harvesters.Models.Contracts
{
    public interface IExtractHcmrEnvoTerm
    {
        string Content { get; set; }

        int[] Types { get; set; }

        string[] Identifiers { get; set; }
    }
}