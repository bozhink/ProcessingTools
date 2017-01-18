namespace ProcessingTools.NlmArchiveConsoleManager.Models
{
    using Contracts.Models;

    public class FileReplacementModel : IFileReplacementModel
    {
        public FileReplacementModel(string destination, string originalFileName, string source)
        {
            this.Destination = destination;
            this.OriginalFileName = originalFileName;
            this.Source = source;
        }

        public string Destination { get; private set; }

        public string OriginalFileName { get; private set; }

        public string Source { get; private set; }
    }
}
