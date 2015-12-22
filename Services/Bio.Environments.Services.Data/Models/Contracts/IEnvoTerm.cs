namespace ProcessingTools.Bio.Environments.Services.Data.Models.Contracts
{
    public interface IEnvoTerm
    {
        string EntityId { get; set; }

        string EnvoId { get; set; }

        string Content { get; set; }
    }
}