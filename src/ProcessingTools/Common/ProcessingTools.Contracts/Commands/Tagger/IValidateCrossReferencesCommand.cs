// <copyright file="IValidateCrossReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Validate Cross-References Command.
    /// </summary>
    public interface IValidateCrossReferencesCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
