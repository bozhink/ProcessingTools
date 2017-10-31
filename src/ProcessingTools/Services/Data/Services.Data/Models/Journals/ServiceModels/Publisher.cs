namespace ProcessingTools.Journals.Services.Data.Models.ServiceModels
{
    using Contracts.Models;

    public class Publisher : IPublisher
    {
        public string AbbreviatedName { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
