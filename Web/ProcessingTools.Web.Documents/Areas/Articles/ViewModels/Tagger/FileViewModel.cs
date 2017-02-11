namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Tagger
{
    using System.Web.Mvc;
    using ViewModels.Files;

    public class FileViewModel : FileDetailsViewModel
    {
        public SelectList CommandId { get; set; }
    }
}
