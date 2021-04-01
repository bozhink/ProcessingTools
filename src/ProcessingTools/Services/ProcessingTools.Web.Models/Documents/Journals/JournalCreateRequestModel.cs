// <copyright file="JournalCreateRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Journals;
    using ProcessingTools.Contracts.Services.Models.Documents.Journals;

    /// <summary>
    /// Journal create request model.
    /// </summary>
    public class JournalCreateRequestModel : IJournalInsertModel, ProcessingTools.Contracts.Models.IWebModel
    {
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
        [RegularExpression(@"^\d{4}-\d{3}[\dxX]$")]
        [StringLength(ValidationConstants.IssnLength)]
        public string PrintIssn { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{4}-\d{3}[\dxX]$")]
        [StringLength(ValidationConstants.IssnLength, MinimumLength = ValidationConstants.IssnLength)]
        public string ElectronicIssn { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string PublisherId { get; set; }

        /// <inheritdoc/>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string JournalStyleId { get; set; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
