﻿// <copyright file="IExtractTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    /// <summary>
    /// Extract Taxa Command.
    /// </summary>
    public interface IExtractTaxaCommand : ITaggerCommand, INotAwaitableCommand
    {
    }
}
