// <copyright file="CoordinatesRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Coordinates
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Coordinates request model.
    /// </summary>
    public class CoordinatesRequestModel : ProcessingTools.Models.Contracts.IWebModel
    {
        /// <summary>
        /// Gets or sets the coordinates text: each coordinate is supposed to be on separate row.
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(2000)]
        public string Coordinates { get; set; }

        /// <inheritdoc/>
        public string ReturnUrl { get; set; }
    }
}
