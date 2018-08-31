namespace ProcessingTools.Web.Documents.Areas.Data.Models.BarcodeGenerator
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants;
    using ValidationConstants = ProcessingTools.Common.Constants.ValidationConstants;

    public class IndexRequestModel
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
