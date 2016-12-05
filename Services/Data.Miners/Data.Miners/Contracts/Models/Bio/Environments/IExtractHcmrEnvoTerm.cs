namespace ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments
{
    public interface IExtractHcmrEnvoTerm
    {
        string Content { get; }

        string[] Identifiers { get; }

        int[] Types { get; }
    }
}
