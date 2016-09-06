namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class JournalViewModel : ModelWithUserInformation
    {
        public JournalViewModel()
            : base()
        {
        }

        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName)]
        [Display(Name = "Abbreviated Name")]
        public string AbbreviatedName { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalId)]
        [Display(Name = "Journal Id")]
        public string JournalId { get; set; }

        [MaxLength(ValidationConstants.IssnLength)]
        [Display(Name = "Print ISSN")]
        public string PrintIssn { get; set; }

        [MaxLength(ValidationConstants.IssnLength)]
        [Display(Name = "Electronic ISSN")]
        public string ElectronicIssn { get; set; }

        [Display(Name = "Publisher")]
        public PublisherViewModel Publisher { get; set; }
    }
}
