// <copyright file="ICommandInfoProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Commands.Models.Contracts;

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
