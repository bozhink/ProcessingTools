// <copyright file="Updater{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Expressions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// Generic updater.
    /// </summary>
    /// <typeparam name="T">Type of the updated object.</typeparam>
    public class Updater<T> : IUpdater<T>
    {
        private readonly IUpdateExpression<T> updateExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="Updater{T}"/> class.
        /// </summary>
        /// <param name="updateExpression">Update expression to be applied.</param>
        public Updater(IUpdateExpression<T> updateExpression)
        {
            this.updateExpression = updateExpression ?? throw new ArgumentNullException(nameof(updateExpression));
        }

        /// <inheritdoc/>
        public IUpdateExpression<T> UpdateExpression => this.updateExpression;

        /// <inheritdoc/>
        public void Invoke(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var type = obj.GetType();

            // TODO: now this works only with Set update command.
            foreach (var updateCommand in this.UpdateExpression.UpdateCommands)
            {
                var property = type.GetProperty(updateCommand.FieldName);
                if (property == null)
                {
                    throw new InvalidOperationException($"Property {updateCommand.FieldName} is not found in type {type.FullName}");
                }

                var method = property.GetSetMethod(true);
                if (method == null)
                {
                    throw new InvalidOperationException($"Set method of property {updateCommand.FieldName} is not found in type {type.FullName}");
                }

                method.Invoke(obj, new[] { updateCommand.Value });
            }
        }

        /// <inheritdoc/>
        public Task InvokeAsync(T obj) => Task.Run(() => this.Invoke(obj));
    }
}
