// <copyright file="CoordinatesResponseViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Coordinates
{
    using System.Collections.Generic;

    /// <summary>
    /// Coordinates response view model.
    /// </summary>
    public class CoordinatesResponseViewModel
    {
        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        public ICollection<CoordinateViewModel> Coordinates { get; } = new List<CoordinateViewModel>();
    }
}
