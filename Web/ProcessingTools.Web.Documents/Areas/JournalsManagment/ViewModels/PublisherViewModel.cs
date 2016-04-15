namespace ProcessingTools.Web.Documents.Areas.JournalsManagment.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PublisherViewModel
    {
        public PublisherViewModel()
        {
            this.DateModified = DateTime.UtcNow;
            this.DateCreated = this.DateModified;
        }

        public Guid Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        [MaxLength(128)]
        public string CreatedByUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [MaxLength(128)]
        public string ModifiedByUserId { get; set; }
    }
}