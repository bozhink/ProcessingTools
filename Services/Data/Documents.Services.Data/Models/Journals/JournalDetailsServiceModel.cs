namespace ProcessingTools.Documents.Services.Data.Models.Journals
{
    using System.Collections.Generic;

    public class JournalDetailsServiceModel : JournalServiceModel
    {
        public JournalDetailsServiceModel()
            : base()
        {
            this.Articles = new HashSet<ArticleServiceModel>();
        }

        public ICollection<ArticleServiceModel> Articles { get; set; }
    }
}
