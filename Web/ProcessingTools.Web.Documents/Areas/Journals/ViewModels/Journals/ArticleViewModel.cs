namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ArticleViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}
