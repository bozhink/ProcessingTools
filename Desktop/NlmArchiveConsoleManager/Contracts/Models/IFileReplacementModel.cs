namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Models
{
    public interface IFileReplacementModel
    {
        string Destination { get; }

        string OriginalFileName { get; }

        string Source { get; }
    }
}
