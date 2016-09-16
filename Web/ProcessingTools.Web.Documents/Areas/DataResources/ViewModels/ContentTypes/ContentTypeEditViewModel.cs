namespace ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;
    using ProcessingTools.DataResources.Data.Common.Constants;

    public class ContentTypeEditViewModel : ContentTypeIndexViewModel, IContentTypeEditViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(ValidationConstants.ContentTypeNameMaximalLength)]
        public override string Name { get; set; }
    }
}
