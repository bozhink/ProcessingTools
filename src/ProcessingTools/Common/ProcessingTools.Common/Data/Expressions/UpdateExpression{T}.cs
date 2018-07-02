// <copyright file="UpdateExpression{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// Generic update expression.
    /// </summary>
    /// <typeparam name="T">Type of the updated field.</typeparam>
    public class UpdateExpression<T> : IUpdateExpression<T>
    {
        private readonly ICollection<IUpdateCommand> updateCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateExpression{T}"/> class.
        /// </summary>
        public UpdateExpression()
        {
            this.updateCommands = new List<IUpdateCommand>();
        }

        /// <inheritdoc/>
        public IEnumerable<IUpdateCommand> UpdateCommands => this.updateCommands;

        /// <inheritdoc/>
        public IUpdateExpression<T> Set(string fieldName, object value)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            this.updateCommands.Add(new UpdateCommand
            {
                FieldName = fieldName,
                UpdateVerb = nameof(this.Set),
                Value = value
            });

            return this;
        }

        /// <inheritdoc/>
        public IUpdateExpression<T> Set<TField>(Expression<Func<T, TField>> field, TField value)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            var member = (MemberExpression)field.Body;
            string fieldName = member.Member.Name;

            return this.Set(fieldName, value);
        }
    }
}
