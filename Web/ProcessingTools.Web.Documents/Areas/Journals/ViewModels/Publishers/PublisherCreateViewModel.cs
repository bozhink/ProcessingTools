namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherCreateViewModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }
    }
}
