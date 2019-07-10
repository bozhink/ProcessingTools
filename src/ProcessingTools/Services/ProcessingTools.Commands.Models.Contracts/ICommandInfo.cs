// <copyright file="ICommandInfo.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;

namespace ProcessingTools.Contracts.Commands.Models
{
    /// <summary>
    /// Command info.
    /// </summary>
    public interface ICommandInfo
    {
        /// <summary>
        /// Gets or sets the command type.
        /// </summary>
        Type CommandType { get; set; }

        /// <summary>
        /// Gets or sets the command name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the command description.
        /// </summary>
        string Description { get; set; }
    }
}
