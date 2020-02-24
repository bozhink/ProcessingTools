﻿// <copyright file="MediatypeDeleteViewModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Files.Mediatypes
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Mediatype delete view model.
    /// </summary>
    public class MediatypeDeleteViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypeDeleteViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        public MediatypeDeleteViewModel(UserContext userContext)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Delete Mediatype")]
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
        /// Gets or sets the file extension.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Extension")]
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Content type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the MIME type.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "MIME type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the MIME subtype.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "MIME subtype")]
        public string MimeSubtype { get; set; }

        /// <summary>
        /// Gets or sets the description of the mediatype.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Description")]
        public string Description { get; set; }

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
        public Uri ReturnUrl { get; set; }
    }
}
