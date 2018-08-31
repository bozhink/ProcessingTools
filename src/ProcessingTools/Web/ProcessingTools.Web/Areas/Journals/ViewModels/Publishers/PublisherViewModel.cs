namespace ProcessingTools.Web.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Journals;
    using ProcessingTools.Web.Areas.Journals.ViewModels.Shared;
    using Strings = ProcessingTools.Web.Resources.Areas.Journals.ViewModels.Publishers.Strings;

    public class PublisherViewModel
    {
        public PublisherViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = this.DateCreated;
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
        [Display(
            Name = nameof(Strings.Id),
            ResourceType = typeof(Strings))]
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
        [Display(
            Name = nameof(Strings.AbbreviatedName),
            ResourceType = typeof(Strings))]
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
        [Display(
            Name = nameof(Strings.Name),
            ResourceType = typeof(Strings))]
        public string Name { get; set; }

        [Display(
            Name = nameof(Strings.CreatedByUser),
            ResourceType = typeof(Strings))]
        public string CreatedByUser { get; set; }

        [Display(
            Name = nameof(Strings.DateCreated),
            ResourceType = typeof(Strings))]
        public DateTime DateCreated { get; set; }

        [Display(
            Name = nameof(Strings.DateModified),
            ResourceType = typeof(Strings))]
        public DateTime DateModified { get; set; }

        [Display(
            Name = nameof(Strings.ModifiedByUser),
            ResourceType = typeof(Strings))]
        public string ModifiedByUser { get; set; }

        [Display(
            Name = nameof(Strings.Addresses),
            ResourceType = typeof(Strings))]
        public ICollection<AddressViewModel> Addresses { get; set; }
    }
}
