// <copyright file="CommandInfo.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Models
{
    using System;
    using ProcessingTools.Commands.Models.Contracts;

    /// <summary>
    /// Command info.
    /// </summary>
    public class CommandInfo : ICommandInfo
    {
        /// <inheritdoc/>
        public Type CommandType { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
