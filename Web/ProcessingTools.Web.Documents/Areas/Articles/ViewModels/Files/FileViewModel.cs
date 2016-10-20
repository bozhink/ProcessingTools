namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class FileViewModel
    {
        [Display(Name = "Document ID")]
        public string DocumentId { get; set; }

        [Display(Name = "Article ID")]
        public string ArticleId { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}
