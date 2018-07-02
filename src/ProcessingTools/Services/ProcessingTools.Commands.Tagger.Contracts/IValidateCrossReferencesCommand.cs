// <copyright file="IValidateCrossReferencesCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    /// <summary>
    /// Validate Cross-References Command.
    /// </summary>
    public interface IValidateCrossReferencesCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
