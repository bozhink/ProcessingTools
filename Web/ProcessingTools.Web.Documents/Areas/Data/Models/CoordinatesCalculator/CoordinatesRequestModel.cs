namespace ProcessingTools.Web.Documents.Areas.Data.Models.CoordinatesCalculator
{
    using System.ComponentModel.DataAnnotations;

    public class CoordinatesRequestModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(2000)]
        public string Coordinates { get; set; }
    }
}
