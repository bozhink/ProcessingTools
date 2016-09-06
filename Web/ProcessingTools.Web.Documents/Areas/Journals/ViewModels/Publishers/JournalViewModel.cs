namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class JournalViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}