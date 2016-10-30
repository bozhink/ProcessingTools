namespace ProcessingTools.Contracts.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IUpdateExpression<T>
    {
        IEnumerable<IUpdateCommand> UpdateCommands { get; }

        IUpdateExpression<T> Set<TField>(Expression<Func<T, TField>> field, TField value);

        IUpdateExpression<T> Set(string fieldName, object value);
    }
}
