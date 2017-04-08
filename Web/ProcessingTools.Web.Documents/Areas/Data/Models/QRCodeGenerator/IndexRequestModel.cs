namespace ProcessingTools.Web.Documents.Areas.Data.Models.QRCodeGenerator
{
    using System.ComponentModel.DataAnnotations;
    using ValidationConstants = ProcessingTools.Constants.Models.ValidationConstants;

    public class IndexRequestModel
    {
        [Required]
        [Range(1, 20)]
        public int PixelPerModule { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.DefaultMaximalLengthOfContent)]
        public string Content { get; set; }
    }
}
