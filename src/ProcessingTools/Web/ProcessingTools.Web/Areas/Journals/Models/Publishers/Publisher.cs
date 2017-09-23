namespace ProcessingTools.Web.Areas.Journals.Models.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;
    using Strings = ProcessingTools.Web.Resources.Areas.Journals.ViewModels.Publishers.Strings;

    public class Publisher : IPublisher, IServiceModel
    {
        public Publisher()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.IdEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfId,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.IdErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string Id { get; set; }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.AbbreviatedNameEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfAbbreviatedPublisherName,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.AbbreviatedNameErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string AbbreviatedName { get; set; }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.NameEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfPublisherName,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.NameErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }
    }
}
