// <copyright file="IModelFactory.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories
{
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Models;

    public interface IModelFactory
    {
        IFileReplacementModel CreateFileReplacementModel(string destination, string originalFileName, string source);
    }
}
