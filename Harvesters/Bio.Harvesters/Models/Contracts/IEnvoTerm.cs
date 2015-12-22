namespace ProcessingTools.Bio.Harvesters.Models.Contracts
{
    public interface IEnvoTerm
    {
        string EntityId { get; set; }

        string EnvoId { get; set; }

        string Content { get; set; }
    }
}
