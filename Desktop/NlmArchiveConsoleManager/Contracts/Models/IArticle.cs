namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Models
{
    using System.Collections.Generic;

    public interface IArticle
    {
        string Doi { get; }

        string FirstPage { get; }

        string Id { get; }

        string Issue { get; }

        string LastPage { get; }

        string Title { get; }

        string Volume { get; }

        ICollection<IAuthor> Authors { get; }
    }
}
