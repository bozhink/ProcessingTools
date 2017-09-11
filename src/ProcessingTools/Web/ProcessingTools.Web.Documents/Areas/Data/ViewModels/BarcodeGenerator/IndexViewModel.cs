namespace ProcessingTools.Web.Documents.Areas.Data.ViewModels.BarcodeGenerator
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ValidationConstants = ProcessingTools.Constants.ValidationConstants;

    public class IndexViewModel
    {
        public IndexViewModel()
            : this(0)
        {
        }

        public IndexViewModel(int selectedValue)
        {
            this.Width = ImagingConstants.DefaultBarcodeWidth;
            this.Height = ImagingConstants.DefaultBarcodeHeight;

            var listItems = typeof(BarcodeType).GetEnumValueTextPairs();
            this.Type = new SelectList(listItems, dataValueField: "Value", dataTextField: "Text", selectedValue: selectedValue);
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
        public SelectList Type { get; set; }

        public object Image { get; set; }
    }
}
