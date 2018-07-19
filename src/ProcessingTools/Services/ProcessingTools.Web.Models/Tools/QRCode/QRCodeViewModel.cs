// <copyright file="QRCodeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.QRCode
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;
    using ValidationConstants = ProcessingTools.Constants.ValidationConstants;

    /// <summary>
    /// QR code view model.
    /// </summary>
    public class QRCodeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QRCodeViewModel"/> class.
        /// </summary>
        public QRCodeViewModel()
        {
            this.PixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
        }

        /// <summary>
        /// Gets or sets the number of pixels per module.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalQRCodePixelsPerModule, ImagingConstants.MaximalQRCodePixelsPerModule)]
        [Display(Name = "Pixels per module", Description = "Pixels per module")]
        public int PixelPerModule { get; set; }

        /// <summary>
        /// Gets or sets the text content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        [Display(Name = "Text to encode", Description = "Text to encode")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the encoded content as image object.
        /// </summary>
        public object Image { get; set; }
    }
}
