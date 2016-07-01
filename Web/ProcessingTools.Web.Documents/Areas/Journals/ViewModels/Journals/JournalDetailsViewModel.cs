namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System.Collections.Generic;

    public class JournalDetailsViewModel : JournalViewModel
    {
        public JournalDetailsViewModel()
            : base()
        {
            this.Articles = new HashSet<ArticleViewModel>();
        }

        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}
