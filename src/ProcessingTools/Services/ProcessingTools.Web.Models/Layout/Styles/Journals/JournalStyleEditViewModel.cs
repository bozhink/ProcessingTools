// <copyright file="JournalStyleEditViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal style edit view model.
    /// </summary>
    public class JournalStyleEditViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStyleEditViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public JournalStyleEditViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Edit Journal Style")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID-s of the selected float object parse styles.
        /// </summary>
        [Display(Name = "Float object parse styles")]
        public IList<string> FloatObjectParseStyleIds { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the ID-s of the selected float object tag styles.
        /// </summary>
        [Display(Name = "Float object tag styles")]
        public IList<string> FloatObjectTagStyleIds { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the ID-s of the selected reference parse styles.
        /// </summary>
        [Display(Name = "Reference parse styles")]
        public IList<string> ReferenceParseStyleIds { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the ID-s of the selected reference tag styles.
        /// </summary>
        [Display(Name = "Reference tag styles")]
        public IList<string> ReferenceTagStyleIds { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the float object parse styles for select.
        /// </summary>
        [Display(Name = "Float object parse styles")]
        public IEnumerable<StyleSelectViewModel> FloatObjectParseStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the float object tag styles for select.
        /// </summary>
        [Display(Name = "Float object tag styles")]
        public IEnumerable<StyleSelectViewModel> FloatObjectTagStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the reference parse styles for select.
        /// </summary>
        [Display(Name = "Reference parse styles")]
        public IEnumerable<StyleSelectViewModel> ReferenceParseStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the reference tag styles for select.
        /// </summary>
        [Display(Name = "Reference tag styles")]
        public IEnumerable<StyleSelectViewModel> ReferenceTagStyles { get; set; } = new List<StyleSelectViewModel>();

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

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
