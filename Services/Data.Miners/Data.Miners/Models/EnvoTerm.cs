namespace ProcessingTools.Data.Miners.Models
{
    using Contracts.Models;

    public class EnvoTerm : IEnvoTerm
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}
