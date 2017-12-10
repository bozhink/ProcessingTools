// <copyright file="IExtractTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Extract Taxa Command.
    /// </summary>
    public interface IExtractTaxaCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
