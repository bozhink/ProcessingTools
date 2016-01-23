namespace ProcessingTools.Bio.Data.Miners.Models.Contracts
{
    public interface IEnvoTerm
    {
        string EntityId { get; set; }

        string EnvoId { get; set; }

        string Content { get; set; }
    }
}
