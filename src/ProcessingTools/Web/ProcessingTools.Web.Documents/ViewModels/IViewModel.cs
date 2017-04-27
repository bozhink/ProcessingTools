namespace ProcessingTools.Web.Documents.ViewModels
{
    using System.Collections.Generic;

    public interface IViewModel
    {
        string Description { get; }

        string Heading { get; }

        string Message { get; }

        IEnumerable<IMetaViewModel> Meta { get; }

        string Title { get; }
    }
}
