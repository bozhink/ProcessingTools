namespace ProcessingTools.Journals.Services.Data.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Journals.Models;
    using ProcessingTools.Contracts.Models;

    internal class PublisherDataModel : IPublisher, IDataModel
    {
        public PublisherDataModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Addresses = new HashSet<IAddress>();
        }

        public string AbbreviatedName { get; set; }

        public ICollection<IAddress> Addresses { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string Id { get; set; }

        public string ModifiedByUser { get; set; }

        public string Name { get; set; }
    }
}
