// <copyright file="ICommandNamesProvider.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Contracts.Providers
{
    using System.Collections.Generic;

    /// <summary>
    /// Command names provider.
    /// </summary>
    public interface ICommandNamesProvider
    {
        /// <summary>
        /// Gets the list of available commands.
        /// </summary>
        IEnumerable<string> CommandNames { get; }
    }
}
