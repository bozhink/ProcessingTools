// <copyright file="BarcodeViewModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Tools.Barcode
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Extensions;
    using ValidationConstants = ProcessingTools.Common.Constants.ValidationConstants;

    /// <summary>
    /// Barcode view model.
    /// </summary>
    public class BarcodeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeViewModel"/> class.
        /// </summary>
        public BarcodeViewModel()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeViewModel"/> class.
        /// </summary>
        /// <param name="selectedType">The selected barcode type.</param>
        public BarcodeViewModel(int selectedType)
        {
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
        public IDictionary<int, string> Type => typeof(BarcodeType).EnumToDictionary();

        /// <summary>
        /// Gets or sets the resultant image.
        /// </summary>
        public object Image { get; set; }
    }
}
