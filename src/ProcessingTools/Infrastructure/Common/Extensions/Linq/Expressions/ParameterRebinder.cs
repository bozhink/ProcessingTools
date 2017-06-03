/// <summary>
/// See https://blogs.msdn.microsoft.com/meek/2008/05/02/linq-to-entities-combining-predicates/
/// </summary>
namespace ProcessingTools.Common.Extensions.Linq.Expressions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(IDictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(IDictionary<ParameterExpression, ParameterExpression> map, Expression expression)
        {
            return new ParameterRebinder(map).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (this.map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }

            return base.VisitParameter(p);
        }
    }
}
