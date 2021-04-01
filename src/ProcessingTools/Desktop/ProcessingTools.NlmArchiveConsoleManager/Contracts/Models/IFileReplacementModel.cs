// <copyright file="IFileReplacementModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Models
{
    public interface IFileReplacementModel
    {
        string Destination { get; }

        string OriginalFileName { get; }

        string Source { get; }
    }
}
