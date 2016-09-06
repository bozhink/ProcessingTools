namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DocumentIdViewModel
    {
        public DocumentIdViewModel(object articleId, object id)
        {
            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            this.Id = id.ToString();
            this.ArticleId = articleId.ToString();
        }

        [Display(Name = "Document ID")]
        public string Id { get; private set; }

        [Display(Name = "Article ID")]
        public string ArticleId { get; private set; }
    }
}
