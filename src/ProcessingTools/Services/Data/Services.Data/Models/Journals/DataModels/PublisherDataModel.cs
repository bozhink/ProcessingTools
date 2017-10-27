namespace ProcessingTools.Journals.Services.Data.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Journals;

    internal class PublisherDataModel : IPublisher
    {
        public PublisherDataModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Addresses = new HashSet<IAddress>();
        }

        public string AbbreviatedName { get; set; }

        public IEnumerable<IAddress> Addresses { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Id { get; set; }

        public string ModifiedBy { get; set; }

        public string Name { get; set; }
    }
}
