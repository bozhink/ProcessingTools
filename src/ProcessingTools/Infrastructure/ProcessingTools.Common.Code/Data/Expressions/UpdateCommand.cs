// <copyright file="UpdateCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.DataAccess.Expressions;

namespace ProcessingTools.Common.Code.Data.Expressions
{
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
