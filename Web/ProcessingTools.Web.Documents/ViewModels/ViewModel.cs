namespace ProcessingTools.Web.Documents.ViewModels
{
    using System.Collections.Generic;

    public class ViewModel : IViewModel
    {
        public string Description { get; set; }

        public string Heading { get; set; }

        public string Message { get; set; }

        public IEnumerable<IMetaViewModel> Meta { get; set; }

        public string Title { get; set; }
    }
}
