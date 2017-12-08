namespace ProcessingTools.Web.Areas.Journals.Models.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Models.Data.Journals;
    using ProcessingTools.Enumerations;
    using Strings = ProcessingTools.Web.Resources.Areas.Journals.ViewModels.Shared.Strings;

    public class Address : IAddress, IServiceModel
    {
        public Address()
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
            ErrorMessageResourceName = nameof(Strings.AddressStringEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfAddressString,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.AddressStringErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }

        public UpdateStatus Status { get; set; } = UpdateStatus.NotModified;
    }
}
