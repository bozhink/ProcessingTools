// <copyright file="IProcessorFactory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using ProcessingTools.Contracts.Models.Documents;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Core;

    public interface IProcessorFactory
    {
        IDirectoryProcessor CreateDirectoryProcessor(string direcoryName, IJournalMeta journalMeta);

        IFileProcessor CreateFileProcessor(string fileName, IJournalMeta journalMeta);
    }
}
