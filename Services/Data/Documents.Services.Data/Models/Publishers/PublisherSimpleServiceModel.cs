namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    using ProcessingTools.Common.Models;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherSimpleServiceModel : ModelWithUserInformation, IPublisherSimpleServiceModel
    {
        public PublisherSimpleServiceModel()
            : base()
        {
        }

        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        public string AbbreviatedName { get; set; }
    }
}
