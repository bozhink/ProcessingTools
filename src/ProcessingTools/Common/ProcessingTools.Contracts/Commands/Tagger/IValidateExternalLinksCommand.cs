// <copyright file="IValidateExternalLinksCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Validate External Links Command.
    /// </summary>
    public interface IValidateExternalLinksCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
