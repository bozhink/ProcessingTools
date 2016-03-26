namespace ProcessingTools.Bio.Environments.Services.Data.Models.Contracts
{
    public interface IEnvoTermServiceModel
    {
        string EntityId { get; set; }

        string EnvoId { get; set; }

        string Content { get; set; }
    }
}