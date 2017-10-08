// <copyright file="IAbbreviation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Models.Abbreviations
{
    public interface IAbbreviation
    {
        string Content { get; }

        string ContentType { get; }

        string Definition { get; }

        string ReplacePattern { get; }

        string SearchPattern { get; }
    }
}
