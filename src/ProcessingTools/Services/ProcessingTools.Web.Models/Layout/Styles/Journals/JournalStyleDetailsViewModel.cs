// <copyright file="JournalStyleDetailsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal style details view model.
    /// </summary>
    public class JournalStyleDetailsViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStyleDetailsViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public JournalStyleDetailsViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Journal Style Details")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the selected float object parse styles.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Float object parse styles")]
        public IList<StyleSelectViewModel> FloatObjectParseStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the selected float object tag styles.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Float object tag styles")]
        public IList<StyleSelectViewModel> FloatObjectTagStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the selected reference parse styles.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Reference parse styles")]
        public IList<StyleSelectViewModel> ReferenceParseStyles { get; set; } = new List<StyleSelectViewModel>();

        /// <summary>
        /// Gets or sets the selected reference tag styles.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Reference tag styles")]
        public IList<StyleSelectViewModel> ReferenceTagStyles { get; set; } = new List<StyleSelectViewModel>();

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
