// <copyright file="ICommandInfoProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using ProcessingTools.Contracts.Commands.Models;

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Command info provider.
    /// </summary>
    public interface ICommandInfoProvider
    {
        /// <summary>
        /// Gets commands information.
        /// </summary>
        IDictionary<Type, ICommandInfo> CommandsInformation { get; }

        /// <summary>
        /// Retrieves information about commands.
        /// </summary>
        void ProcessInformation();
    }
}
