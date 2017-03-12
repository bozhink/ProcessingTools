namespace ProcessingTools.Web.Areas.Journals.ViewModels.Publishers
{
    using ProcessingTools.Journals.Data.Common.Contracts.Models;
    using ProcessingTools.Journals.Data.Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PublisherViewModel : IPublisher
    {
        public PublisherViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = this.DateCreated;
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        [Display(Name = "ID", Description = "ID")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        [Display(Name = "Abbreviated Name", Description = "Abbreviated Name")]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        [Display(Name = "Name", Description = "Name")]
        public string Name { get; set; }

        [Display(Name = "Created By", Description = "Created By")]
        public string CreatedByUser { get; set; }

        [Display(Name = "Date Created", Description = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified", Description = "Date Modified")]
        public DateTime DateModified { get; set; }

        [Display(Name = "Modified By", Description = "Modified By")]
        public string ModifiedByUser { get; set; }

        [Display(Name = "Addresses", Description = "Addresses")]
        public ICollection<IAddress> Addresses { get; set; }
    }
}
