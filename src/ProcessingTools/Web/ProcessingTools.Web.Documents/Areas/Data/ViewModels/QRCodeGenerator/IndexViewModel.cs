﻿namespace ProcessingTools.Web.Documents.Areas.Data.ViewModels.QRCodeGenerator
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;
    using ValidationConstants = ProcessingTools.Constants.ValidationConstants;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.PixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
        }

        [Required]
        [Range(ImagingConstants.MinimalQRCodePixelsPerModule, ImagingConstants.MaximalQRCodePixelsPerModule)]
        [Display(Name = "Pixels per module", Description = "Pixels per module")]
        public int PixelPerModule { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        [Display(Name = "Text to encode", Description = "Text to encode")]
        public string Content { get; set; }

        public object Image { get; set; }
    }
}
