// <copyright file="IUpdateExpression.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Generic update expression.
    /// </summary>
    /// <typeparam name="T">Type of the updated field.</typeparam>
    public interface IUpdateExpression<T>
    {
        /// <summary>
        /// Gets the list of update commands.
        /// </summary>
        IEnumerable<IUpdateCommand> UpdateCommands { get; }

        /// <summary>
        /// Execute Set command.
        /// </summary>
        /// <typeparam name="TField">Type of the field to be updated</typeparam>
        /// <param name="field">Field selector</param>
        /// <param name="value">Value to be set</param>
        /// <returns>Update expression to be chained</returns>
        IUpdateExpression<T> Set<TField>(Expression<Func<T, TField>> field, TField value);

        /// <summary>
        /// Execute Set command.
        /// </summary>
        /// <param name="fieldName">Name of the field for update</param>
        /// <param name="value">Value to be set</param>
        /// <returns>Update expression to be chained</returns>
        IUpdateExpression<T> Set(string fieldName, object value);
    }
}
