// <copyright file="BarcodeRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
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
        [Required]
        [Range(ImagingConstants.MinimalBarcodeWidth, ImagingConstants.MaximalBarcodeWidth)]
        public int Width { get; set; }

        [Required]
        [Range(ImagingConstants.MinimalBarcodeHeight, ImagingConstants.MaximalBarcodeHeight)]
        public int Height { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        public string Content { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
