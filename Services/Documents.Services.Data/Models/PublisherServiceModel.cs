namespace ProcessingTools.Documents.Services.Data.Models
{
    using System;

    public class PublisherServiceModel
    {
        public PublisherServiceModel()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string ModifiedByUserId { get; set; }
    }
}