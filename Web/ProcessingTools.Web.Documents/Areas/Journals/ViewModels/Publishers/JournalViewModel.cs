namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class JournalViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        public string Name { get; set; }
    }
}