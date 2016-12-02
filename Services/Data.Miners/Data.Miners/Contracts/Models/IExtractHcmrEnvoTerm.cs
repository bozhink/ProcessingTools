namespace ProcessingTools.Data.Miners.Contracts.Models
{
    public interface IExtractHcmrEnvoTerm
    {
        string Content { get; }

        string[] Identifiers { get; }

        int[] Types { get; }
    }
}
