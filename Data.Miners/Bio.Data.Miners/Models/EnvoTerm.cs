namespace ProcessingTools.Bio.Data.Miners.Models
{
    using Contracts;

    public class EnvoTerm : IEnvoTerm
    {
        public string EntityId { get; set; }

        public string EnvoId { get; set; }

        public string Content { get; set; }
    }
}