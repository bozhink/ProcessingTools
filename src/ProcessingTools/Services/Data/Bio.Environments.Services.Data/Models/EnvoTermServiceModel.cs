namespace ProcessingTools.Bio.Environments.Services.Data.Models
{
    using ProcessingTools.Models.Contracts.Bio;

    public class EnvoTermServiceModel : IEnvoTerm
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}
