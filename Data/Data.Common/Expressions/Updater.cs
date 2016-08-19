namespace ProcessingTools.Data.Common.Expressions
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    public class Updater<TEntity, TDbModel> : IUpdater<TEntity, TDbModel>
        where TDbModel : TEntity
    {
        private readonly IUpdateExpression<TEntity> updateExpression;

        public Updater(IUpdateExpression<TEntity> updateExpression)
        {
            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            this.updateExpression = updateExpression;
        }

        public IUpdateExpression<TEntity> UpdateExpression => this.updateExpression;

        public Task<TDbModel> Invoke(TDbModel obj) => Task.Run(() =>
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

                method.Invoke(obj, new object[] { updateCommand.Value });
            }

            return obj;
        });
    }
}
