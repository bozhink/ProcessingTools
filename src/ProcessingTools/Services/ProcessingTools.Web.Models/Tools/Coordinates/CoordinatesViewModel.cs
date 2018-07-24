// <copyright file="CoordinatesViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Coordinates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Coordinates view model.
    /// </summary>
    public class CoordinatesViewModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesViewModel"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="coordinates">Coordinates to present as list.</param>
        public CoordinatesViewModel(UserContext userContext, IEnumerable<CoordinateViewModel> coordinates)
        {
            this.UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            this.Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }

        /// <summary>
        /// Gets or sets the page heading.
        /// </summary>
        [Display(Name = "Coordinates")]
        public string PageHeading { get; set; }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        public UserContext UserContext { get; }

        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        [Display(Name = "Coordinates")]
        public IEnumerable<CoordinateViewModel> Coordinates { get; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
