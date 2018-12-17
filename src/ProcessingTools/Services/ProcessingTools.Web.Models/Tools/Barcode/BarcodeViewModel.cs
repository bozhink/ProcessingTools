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
        public BarcodeViewModel()
            : this(0)
        {
        }

        public BarcodeViewModel(int selectedValue)
        {
            this.Width = ImagingConstants.DefaultBarcodeWidth;
            this.Height = ImagingConstants.DefaultBarcodeHeight;
        }

        [Required]
        [Range(ImagingConstants.MinimalBarcodeWidth, ImagingConstants.MaximalBarcodeWidth)]
        [Display(Name = "Barcode Width", Description = "Barcode Width")]
        public int Width { get; set; }

        [Required]
        [Range(ImagingConstants.MinimalBarcodeHeight, ImagingConstants.MaximalBarcodeHeight)]
        [Display(Name = "Barcode Height", Description = "Barcode Height")]
        public int Height { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        [Display(Name = "Text to encode", Description = "Text to encode")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Barcode Type", Description = "Barcode Type")]
        public IDictionary<int, string> Type => typeof(BarcodeType).EnumToDictionary();

        public object Image { get; set; }
    }
}
