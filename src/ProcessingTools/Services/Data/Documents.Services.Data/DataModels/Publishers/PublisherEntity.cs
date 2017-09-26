namespace ProcessingTools.Documents.Services.Data.DataModels.Publishers
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Models.Abstractions;

    public class PublisherEntity : ModelWithUserInformation, IPublisher
    {
        public PublisherEntity()
        {
            this.Id = Guid.NewGuid();
            this.Addresses = new HashSet<IAddress>();
        }

        public Guid Id { get; set; }

        public string AbbreviatedName { get; set; }

        public string Name { get; set; }

        public IEnumerable<IAddress> Addresses { get; set; }
    }
}
