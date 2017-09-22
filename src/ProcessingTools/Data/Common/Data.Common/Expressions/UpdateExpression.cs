namespace ProcessingTools.Data.Common.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts;

    public class UpdateExpression<T> : IUpdateExpression<T>
    {
        private readonly ICollection<IUpdateCommand> updateCommands;

        public UpdateExpression()
        {
            this.updateCommands = new List<IUpdateCommand>();
        }

        public IEnumerable<IUpdateCommand> UpdateCommands => this.updateCommands;

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
