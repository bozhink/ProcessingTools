namespace ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Files
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DocumentViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "File Extension")]
        public string FileExtension { get; set; }

        [Display(Name = "Content Length")]
        public long ContentLength { get; set; }

        [Display(Name = "Content Type")]
        public string ContentType { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}
