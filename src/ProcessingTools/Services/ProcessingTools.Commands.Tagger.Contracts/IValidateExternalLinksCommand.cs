﻿// <copyright file="IValidateExternalLinksCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    /// <summary>
    /// Validate External Links Command.
    /// </summary>
    public interface IValidateExternalLinksCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}