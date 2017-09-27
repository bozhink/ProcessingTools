namespace ProcessingTools.Journals.Data.Entity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Models.Contracts.Journals;

    public class Publisher : Addressable, IPublisher
    {
        public Publisher()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }
    }
}
