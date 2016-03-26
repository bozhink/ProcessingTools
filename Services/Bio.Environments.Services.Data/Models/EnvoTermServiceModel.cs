namespace ProcessingTools.Bio.Environments.Services.Data.Models
{
    using Contracts;

    public class EnvoTermServiceModel : IEnvoTermServiceModel
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}