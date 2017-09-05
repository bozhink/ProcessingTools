namespace ProcessingTools.Bio.Environments.Services.Data.Models
{
    using ProcessingTools.Contracts.Models.Bio.Environments;

    public class EnvoTermServiceModel : IEnvoTermServiceModel
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}
