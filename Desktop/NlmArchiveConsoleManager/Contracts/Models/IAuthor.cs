namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Models
{
    public interface IAuthor
    {
        string GivenNames { get; }

        string Prefix { get; }

        string Suffix { get; }

        string Surname { get; }
    }
}
