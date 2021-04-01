// <copyright file="ICommandInfoProvider.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Commands.Models;

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
