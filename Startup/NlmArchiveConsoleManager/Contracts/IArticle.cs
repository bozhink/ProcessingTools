namespace ProcessingTools.NlmArchiveConsoleManager.Contracts
{
    using System.Collections.Generic;

    public interface IArticle
    {
        string Id { get; set; }

        string Doi { get; set; }

        string Title { get; set; }

        string Volume { get; set; }

        string Issue { get; set; }

        string FirstPage { get; set; }

        string LastPage { get; set; }

        ICollection<IAuthor> Authors { get; set; }
    }
}