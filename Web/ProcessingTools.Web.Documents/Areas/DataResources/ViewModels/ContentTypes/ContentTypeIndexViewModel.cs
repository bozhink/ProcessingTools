namespace ProcessingTools.Web.Documents.Areas.DataResources.ViewModels.ContentTypes
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class ContentTypeIndexViewModel : IContentTypeIndexViewModel
    {
        public virtual int Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Name { get; set; }
    }
}
