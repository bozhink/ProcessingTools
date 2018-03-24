// <copyright file="ICommandInfoProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands
{
    using System;
    using System.Collections.Generic;

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
