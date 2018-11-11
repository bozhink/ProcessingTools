// <copyright file="QRCodeRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.QRCode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ValidationConstants = ProcessingTools.Common.Constants.ValidationConstants;

    /// <summary>
    /// QR code request model
    /// </summary>
    public class QRCodeRequestModel
    {
        /// <summary>
        /// Gets or sets the number of pixels per module.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalQRCodePixelsPerModule, ImagingConstants.MaximalQRCodePixelsPerModule)]
        public int PixelPerModule { get; set; }

        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        public string Content { get; set; }
    }
}
