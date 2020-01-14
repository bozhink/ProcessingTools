// <copyright file="BarcodeRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Barcode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ValidationConstants = ProcessingTools.Common.Constants.ValidationConstants;

    /// <summary>
    /// Barcode request model.
    /// </summary>
    public class BarcodeRequestModel
    {
        /// <summary>
        /// Gets or sets the width of the barcode.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalBarcodeWidth, ImagingConstants.MaximalBarcodeWidth)]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the barcode.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalBarcodeHeight, ImagingConstants.MaximalBarcodeHeight)]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the type of the barcode.
        /// </summary>
        [Required]
        public int Type { get; set; }
    }
}
