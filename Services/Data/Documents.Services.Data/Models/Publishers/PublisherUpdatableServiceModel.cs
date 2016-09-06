namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherUpdatableServiceModel : IPublisherUpdatableServiceModel
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }
    }
}
