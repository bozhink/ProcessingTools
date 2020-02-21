// <copyright file="BarcodeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ValidationConstants = ProcessingTools.Common.Constants.ValidationConstants;

    /// <summary>
    /// Barcode view model.
    /// </summary>
    public class BarcodeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeViewModel"/> class.
        /// </summary>
        /// <param name="types">List of barcode types.</param>
        public BarcodeViewModel(IReadOnlyDictionary<int, string> types)
            : this((int)BarcodeType.Unspecified, types)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeViewModel"/> class.
        /// </summary>
        /// <param name="selectedType">The selected barcode type.</param>
        /// <param name="types">List of barcode types.</param>
        public BarcodeViewModel(int selectedType, IReadOnlyDictionary<int, string> types)
        {
            this.Types = types ?? throw new ArgumentNullException(nameof(types));
            this.SelectedType = selectedType;
            this.Width = ImagingConstants.DefaultBarcodeWidth;
            this.Height = ImagingConstants.DefaultBarcodeHeight;
        }

        /// <summary>
        /// Gets or sets the width of the resultant barcode.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalBarcodeWidth, ImagingConstants.MaximalBarcodeWidth)]
        [Display(Name = "Barcode Width", Description = "Barcode Width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the resultant barcode.
        /// </summary>
        [Required]
        [Range(ImagingConstants.MinimalBarcodeHeight, ImagingConstants.MaximalBarcodeHeight)]
        [Display(Name = "Barcode Height", Description = "Barcode Height")]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the content to be encoded.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        [Display(Name = "Text to encode", Description = "Text to encode")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        public int SelectedType { get; set; }

        /// <summary>
        /// Gets the collection of barcode types.
        /// </summary>
        [Required]
        [Display(Name = "Barcode Type", Description = "Barcode Type")]
        public IReadOnlyDictionary<int, string> Types { get; }

        /// <summary>
        /// Gets or sets the resultant image.
        /// </summary>
        public object Image { get; set; }
    }
}
