// <copyright file="JournalEditViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Documents.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using ProcessingTools.Constants.Data.Journals;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal edit view model.
    /// </summary>
    public class JournalEditViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalEditViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="publishers">Publisher for combo box.</param>
        public JournalEditViewModel(UserContext userContext, IEnumerable<JournalPublisherViewModel> publishers)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Publishers = publishers ?? throw new ArgumentNullException(nameof(publishers));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Edit Journal")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(ValidationConstants.MaximalLengthOfId, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = ValidationConstants.MinimalLengthOfId)]
        [Display(Name = "ID")]
        public string Id { get; set; }

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
        [RegularExpression(@"\A(\d{4}-\d{3}[\dxX]|\s+)\Z")]
        [StringLength(ValidationConstants.IssnLength, ErrorMessage = "The {0} must be {1} characters long.")]
        [Display(Name = "Print ISSN")]
        public string PrintIssn { get; set; }

        /// <summary>
        /// Gets or sets the journal's electronic ISSN.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"\A\d{4}-\d{3}[\dxX]\Z")]
        [StringLength(ValidationConstants.IssnLength, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = ValidationConstants.IssnLength)]
        [Display(Name = "Electronic ISSN")]
        public string ElectronicIssn { get; set; }

        /// <summary>
        /// Gets publishers for combo box.
        /// </summary>
        public IEnumerable<JournalPublisherViewModel> Publishers { get; }

        /// <summary>
        /// Gets the ID of the publisher of the journal.
        /// </summary>
        [Required]
        [Display(Name = "Publisher")]
        public string PublisherId => this.Publishers?.FirstOrDefault(p => p.Selected)?.Id;

        /// <summary>
        /// Gets or sets created by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets created on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets modified by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified by")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets modified on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified on")]
        public DateTime ModifiedOn { get; set; }
    }
}
