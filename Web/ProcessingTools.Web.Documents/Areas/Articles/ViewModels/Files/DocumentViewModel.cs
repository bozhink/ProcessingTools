namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DocumentViewModel : DocumentIdViewModel
    {
        public DocumentViewModel(object articleId, object id)
            : base(articleId, id)
        {
        }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}
