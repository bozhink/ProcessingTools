namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System.ComponentModel.DataAnnotations;

    public class FileDetailsViewModel : FileViewModel
    {
        public FileDetailsViewModel(object articleId, object id)
            : base(articleId, id)
        {
        }

        [Display(Name = "File Extension")]
        public string FileExtension { get; set; }

        [Display(Name = "Content Length")]
        public long ContentLength { get; set; }

        [Display(Name = "Content Type")]
        public string ContentType { get; set; }
    }
}
