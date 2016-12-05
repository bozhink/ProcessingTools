using ProcessingTools.Data.Miners.Contracts.Models.Bio.Environments;

namespace ProcessingTools.Data.Miners.Models.Bio.Environments
{
    public class EnvoTerm : IEnvoTerm
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}
