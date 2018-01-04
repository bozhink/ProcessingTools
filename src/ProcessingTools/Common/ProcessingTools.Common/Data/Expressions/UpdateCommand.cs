// <copyright file="UpdateCommand.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Expressions
{
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// Update Command.
    /// </summary>
    public sealed class UpdateCommand : IUpdateCommand
    {
        /// <inheritdoc/>
        public string FieldName { get; set; }

        /// <inheritdoc/>
        public string UpdateVerb { get; set; }

        /// <inheritdoc/>
        public object Value { get; set; }
    }
}
