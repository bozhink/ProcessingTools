// <copyright file="IProcessingCommandModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Processing command model.
    /// </summary>
    public interface IProcessingCommandModel
    {
        /// <summary>
        /// Gets the assembly of the processing command.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Gets the type of the processing command.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the category of the processing command.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// Gets the name of the processing command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the additional parameters of the processing command.
        /// </summary>
        IList<object> Parameters { get; }
    }
}
