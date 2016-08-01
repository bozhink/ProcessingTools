namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PublisherSimpleViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
