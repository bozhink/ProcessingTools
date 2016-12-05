namespace ProcessingTools.Data.Miners.Models.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Models.Bio;

    public class BiorepositoriesCollection : IBiorepositoriesCollection
    {
        public string CollectionCode { get; set; }

        public string CollectionName { get; set; }

        public string Url { get; set; }
    }
}
