﻿// <copyright file="IValidateTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Validate Taxa Command.
    /// </summary>
    public interface IValidateTaxaCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
