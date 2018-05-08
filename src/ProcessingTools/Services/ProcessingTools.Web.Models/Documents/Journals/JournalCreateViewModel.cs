// <copyright file="JournalCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal create view model.
    /// </summary>
    public class JournalCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="publishers">Publishers for select.</param>
        /// <param name="journalStyles">Journal styles for select.</param>
        public JournalCreateViewModel(UserContext userContext, IEnumerable<JournalPublisherViewModel> publishers, IEnumerable<JournalStyleViewModel> journalStyles)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Publishers = publishers ?? throw new ArgumentNullException(nameof(publishers));
            this.JournalStyles = journalStyles ?? throw new ArgumentNullException(nameof(journalStyles));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create New Journal")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfAbbreviatedJournalName, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfAbbreviatedJournalName)]
        [Display(Name = "Abbreviated name")]
        public string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalName, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfJournalName)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the journal's ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfJournalId, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfJournalId)]
        [Display(Name = "Journal ID")]
        public string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the journal's print ISSN.
        /// </summary>
        [RegularExpression(@"^\d{4}-\d{3}[\dxX]$")]
        [StringLength(ValidationConstants.IssnLength, ErrorMessage = "The {0} must be {1} characters long.")]
        [Display(Name = "Print ISSN")]
        public string PrintIssn { get; set; }

        /// <summary>
        /// Gets or sets the journal's electronic ISSN.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{4}-\d{3}[\dxX]$")]
        [StringLength(ValidationConstants.IssnLength, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = ValidationConstants.IssnLength)]
        [Display(Name = "Electronic ISSN")]
        public string ElectronicIssn { get; set; }

        /// <summary>
        /// Gets publishers for select.
        /// </summary>
        public IEnumerable<JournalPublisherViewModel> Publishers { get; }

        /// <summary>
        /// Gets or sets the ID of the publisher of the journal.
        /// </summary>
        [Required]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        [Display(Name = "Publisher")]
        public string PublisherId { get; set; }

        /// <summary>
        /// Gets the journal styles.
        /// </summary>
        public IEnumerable<JournalStyleViewModel> JournalStyles { get; }

        /// <summary>
        /// Gets or sets the ID of the journal style.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, MinimumLength = ValidationConstants.MinimalLengthOfId)]
        [Display(Name = "Journal style")]
        public string JournalStyleId { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
