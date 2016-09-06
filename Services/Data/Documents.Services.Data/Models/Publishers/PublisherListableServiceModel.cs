namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using Contracts;

    public class PublisherListableServiceModel : IPublisherListableServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
