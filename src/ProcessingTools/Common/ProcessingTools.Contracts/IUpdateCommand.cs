// <copyright file="IUpdateCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Update command.
    /// </summary>
    public interface IUpdateCommand
    {
        /// <summary>
        /// Gets name of the field.
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Gets the update command verb.
        /// </summary>
        string UpdateVerb { get; }

        /// <summary>
        /// Gets the value for to update.
        /// </summary>
        object Value { get; }
    }
}
