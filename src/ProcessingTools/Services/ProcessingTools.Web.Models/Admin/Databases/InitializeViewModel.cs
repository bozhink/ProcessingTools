// <copyright file="InitializeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Admin.Databases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Initialize view model.
    /// </summary>
    public class InitializeViewModel : ProcessingTools.Contracts.Models.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="exceptions">Exceptions during initialization.</param>
        public InitializeViewModel(UserContext userContext, IEnumerable<Exception> exceptions)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Exceptions = exceptions;
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Initialized databases")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets or sets the number of databases.
        /// </summary>
        public int NumberOfDatabases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether initialization is successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        public IEnumerable<Exception> Exceptions { get; }

        /// <inheritdoc/>
        public Uri ReturnUrl { get; set; }
    }
}
