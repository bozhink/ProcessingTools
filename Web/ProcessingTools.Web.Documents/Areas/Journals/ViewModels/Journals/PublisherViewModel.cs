namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Journals
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PublisherViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Abbreviated Name")]
        public string AbbreviatedName { get; set; }
    }
}
