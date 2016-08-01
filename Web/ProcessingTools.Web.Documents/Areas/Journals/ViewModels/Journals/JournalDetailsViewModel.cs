namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class JournalDetailsViewModel : JournalViewModel
    {
        public JournalDetailsViewModel()
            : base()
        {
            this.Articles = new HashSet<ArticleViewModel>();
        }

        [Display(Name = "Articles")]
        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}
