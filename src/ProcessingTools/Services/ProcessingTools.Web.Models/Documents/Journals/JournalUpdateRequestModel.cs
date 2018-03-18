// <copyright file="JournalUpdateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journal edit request model.
    /// </summary>
    public class JournalUpdateRequestModel : IJournalUpdateModel
    {
        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName, MinimumLength = ValidationConstants.MinimalLengthOfAbbreviatedJournalName)]
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalName, MinimumLength = ValidationConstants.MinimalLengthOfJournalName)]
        public string Name { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalId, MinimumLength = ValidationConstants.MinimalLengthOfJournalId)]
        public string JournalId { get; set; }

        /// <inheritdoc/>
        [RegularExpression(@"\A(\d{4}-\d{3}[\dxX]|\s+)\Z")]
        [StringLength(ValidationConstants.IssnLength)]
        public string PrintIssn { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"\A\d{4}-\d{3}[\dxX]\Z")]
        [StringLength(ValidationConstants.IssnLength, MinimumLength = ValidationConstants.IssnLength)]
        public string ElectronicIssn { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string PublisherId { get; set; }
    }
}
