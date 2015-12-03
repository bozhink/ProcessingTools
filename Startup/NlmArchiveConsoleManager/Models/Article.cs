namespace ProcessingTools.NlmArchiveConsoleManager.Models
{
    using System.Collections.Generic;
    using Contracts;
    public class Article : IArticle
    {
        public ICollection<IAuthor> Authors { get; set; }

        public string Doi { get; set; }

        public string FirstPage { get; set; }

        public string Id { get; set; }

        public string Issue { get; set; }

        public string LastPage { get; set; }

        public string Title { get; set; }

        public string Volume { get; set; }
    }
}
