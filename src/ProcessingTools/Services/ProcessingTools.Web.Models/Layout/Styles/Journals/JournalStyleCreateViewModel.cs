// <copyright file="JournalStyleCreateViewModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal style create view model.
    /// </summary>
    public class JournalStyleCreateViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStyleCreateViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public JournalStyleCreateViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Create Journal Style")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

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

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
