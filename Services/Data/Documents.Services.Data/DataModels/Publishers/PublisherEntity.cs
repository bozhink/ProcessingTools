namespace ProcessingTools.Documents.Services.Data.DataModels.Publishers
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Common.Models;
    using ProcessingTools.Contracts.Data.Documents.Models;

    public class PublisherEntity : ModelWithUserInformation, IPublisherEntity
    {
        public PublisherEntity()
        {
            this.Id = Guid.NewGuid();
            this.Addresses = new HashSet<IAddressEntity>();
        }

        public Guid Id { get; set; }

        public string AbbreviatedName { get; set; }

        public string Name { get; set; }

        public ICollection<IAddressEntity> Addresses { get; set; }
    }
}
