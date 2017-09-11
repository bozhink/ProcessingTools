namespace ProcessingTools.Web.Documents.Areas.Data.Models.QRCodeGenerator
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants;
    using ValidationConstants = ProcessingTools.Constants.ValidationConstants;

    public class IndexRequestModel
    {
        [Required]
        [Range(ImagingConstants.MinimalQRCodePixelsPerModule, ImagingConstants.MaximalQRCodePixelsPerModule)]
        public int PixelPerModule { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        public string Content { get; set; }
    }
}
