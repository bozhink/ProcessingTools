namespace ProcessingTools.Web.Areas.Data.Models.Shared
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Enumerations;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.Shared.Models_Strings;

    public class SynonymRequestModel : ISynonym
    {
        public int Id { get; set; }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.NameEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfSynonymName,
            MinimumLength = ValidationConstants.MinimalLengthOfSynonymName,
            ErrorMessageResourceName = nameof(Strings.NameErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }

        public int? LanguageCode { get; set; }

        public UpdateStatus Status { get; set; } = UpdateStatus.NotModified;

        public int ParentId { get; set; } = -1;
    }
}