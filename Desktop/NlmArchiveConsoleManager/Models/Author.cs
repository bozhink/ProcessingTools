namespace ProcessingTools.NlmArchiveConsoleManager.Models
{
    using Contracts.Models;

    public class Author : IAuthor
    {
        public string GivenNames { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string Surname { get; set; }
    }
}