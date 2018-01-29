// <copyright file="JournalEditRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;

    /// <summary>
    /// Journal edit request model.
    /// </summary>
    public class JournalEditRequestModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName, MinimumLength = ValidationConstants.MinimalLengthOfAbbreviatedJournalName)]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalName, MinimumLength = ValidationConstants.MinimalLengthOfJournalName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the journal's ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalId, MinimumLength = ValidationConstants.MinimalLengthOfJournalId)]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the journal's print ISSN.
        /// </summary>
        [RegularExpression(@"\A(\d{4}-\d{3}[\dxX]|\s+)\Z")]
        [StringLength(ValidationConstants.IssnLength)]
        public string PrintIssn { get; set; }

        /// <summary>
        /// Gets or sets the journal's electronic ISSN.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"\A\d{4}-\d{3}[\dxX]\Z")]
        [StringLength(ValidationConstants.IssnLength, MinimumLength = ValidationConstants.IssnLength)]
        public string ElectronicIssn { get; set; }

        /// <summary>
        /// Gets or sets the ID of the publisher of the journal.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        public string PublisherId { get; set; }
    }
}
