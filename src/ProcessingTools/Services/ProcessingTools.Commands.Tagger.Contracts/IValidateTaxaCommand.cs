﻿// <copyright file="IValidateTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    /// <summary>
    /// Validate Taxa Command.
    /// </summary>
    public interface IValidateTaxaCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
