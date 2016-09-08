namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Documents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Contracts;

    public class DocumentIdViewModel : IDocumentIdViewModel
    {
        private string articleId;
        private string documentId;

        public DocumentIdViewModel(object articleId, object documentId)
        {
            this.DocumentId = documentId.ToString();
            this.ArticleId = articleId.ToString();
        }

        [Display(Name = "Document ID")]
        public string DocumentId
        {
            get
            {
                return this.documentId;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.DocumentId));
                }

                this.documentId = value;
            }
        }

        [Display(Name = "Article ID")]
        public string ArticleId
        {
            get
            {
                return this.articleId;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.ArticleId));
                }

                this.articleId = value;
            }
        }
    }
}
