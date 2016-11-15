namespace ProcessingTools.NlmArchiveConsoleManager.Models
{
    using Contracts.Models;

    public class FileReplacementModel : IFileReplacementModel
    {
        public string Destination { get; set; }

        public string OriginalFileName { get; set; }

        public string Source { get; set; }
    }
}
