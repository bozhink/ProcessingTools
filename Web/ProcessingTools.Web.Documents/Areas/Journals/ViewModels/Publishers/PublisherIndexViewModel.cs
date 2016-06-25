namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;

    public class PublisherIndexViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AbbreviatedName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
