namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using Contracts.Core;
    using ProcessingTools.Contracts.Models.Documents;

    public interface IProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournalMeta journalMeta);

        IFileProcessor CreateFileProcessor(string fileName, IJournalMeta journalMeta);
    }
}
