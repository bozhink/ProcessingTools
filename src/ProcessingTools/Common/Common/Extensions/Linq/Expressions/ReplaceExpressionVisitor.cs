namespace ProcessingTools.Common.Extensions.Linq.Expressions
{
    using System.Linq.Expressions;

    /// <summary>
    /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
    /// </summary>
    public class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression oldValue;
        private readonly Expression newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == this.oldValue)
            {
                return this.newValue;
            }

            return base.Visit(node);
        }
    }
}
