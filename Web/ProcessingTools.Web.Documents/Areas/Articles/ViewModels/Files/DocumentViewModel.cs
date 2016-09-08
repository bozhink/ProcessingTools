namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DocumentViewModel
    {
        public DocumentViewModel(object articleId, object id)
        {
            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.DocumentId = id.ToString();
            this.ArticleId = articleId.ToString();
        }

        [Display(Name = "Document ID")]
        public string DocumentId { get; private set; }

        [Display(Name = "Article ID")]
        public string ArticleId { get; private set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}
