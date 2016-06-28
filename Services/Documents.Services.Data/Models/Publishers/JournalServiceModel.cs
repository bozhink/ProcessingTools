namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class JournalServiceModel
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfJournalName)]
        public string Name { get; set; }
    }
}
