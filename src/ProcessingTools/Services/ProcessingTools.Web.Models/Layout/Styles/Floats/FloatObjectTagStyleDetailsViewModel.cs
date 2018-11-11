// <copyright file="FloatObjectTagStyleDetailsViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Float object tag style details view model.
    /// </summary>
    public class FloatObjectTagStyleDetailsViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStyleDetailsViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public FloatObjectTagStyleDetailsViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Float Object Tag Style Details")]
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
        /// Gets or sets the reference type of the floating object according to NLM schema.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Reference type")]
        public ReferenceType FloatReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the name in the label of the floating object.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Float type name in label")]
        public string FloatTypeNameInLabel { get; set; }

        /// <summary>
        /// Gets or sets the regex pattern to match citations of the floating object in text.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Match citation pattern")]
        public string MatchCitationPattern { get; set; }

        /// <summary>
        /// Gets or sets the value of the xref/@ref-type which will be used only during the tagging process.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Internal reference type")]
        public string InternalReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the value of the xref/@ref-type which will be used in the final XML.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Resultant reference type")]
        public string ResultantReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which provide information about the floating object.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Float object XPath")]
        public string FloatObjectXPath { get; set; }

        /// <summary>
        /// Gets or sets the target XPath.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Target XPath")]
        public string TargetXPath { get; set; }

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
