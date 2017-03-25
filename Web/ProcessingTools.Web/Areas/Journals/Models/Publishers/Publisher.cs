namespace ProcessingTools.Web.Areas.Journals.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Journals.Data.Common.Constants;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;

    public class Publisher : IPublisher, IServiceModel
    {
        public Publisher()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }
    }
}
