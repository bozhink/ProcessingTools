﻿// <copyright file="UpdateCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Data.Expressions
{
    using ProcessingTools.Contracts.DataAccess.Expressions;

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
