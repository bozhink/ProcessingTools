namespace ProcessingTools.Data.Miners.Models
{
    using Contracts.Models;

    public class BiorepositoriesCollection : IBiorepositoriesCollection
    {
        public string CollectionCode { get; set; }

        public string CollectionName { get; set; }

        public string Url { get; set; }
    }
}
