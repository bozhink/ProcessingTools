namespace ProcessingTools.Data.Common.Expressions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Expressions.Contracts;

    public class Updater<T> : IUpdater<T>
    {
        private readonly IUpdateExpression<T> updateExpression;

        public Updater(IUpdateExpression<T> updateExpression)
        {
            this.updateExpression = updateExpression ?? throw new ArgumentNullException(nameof(updateExpression));
        }

        public IUpdateExpression<T> UpdateExpression => this.updateExpression;

        public async Task Invoke(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            await Task.Run(() =>
            {
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
            })
            .ConfigureAwait(false);
        }
    }
}
