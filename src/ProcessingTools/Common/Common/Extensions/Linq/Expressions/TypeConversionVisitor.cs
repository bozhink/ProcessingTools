/// <summary>
/// See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
/// </summary>
namespace ProcessingTools.Common.Extensions.Linq.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class TypeConversionVisitor : ExpressionVisitor
    {
        private readonly IDictionary<Expression, Expression> parameterMap;

        public TypeConversionVisitor(IDictionary<Expression, Expression> parameterMap)
        {
            this.parameterMap = parameterMap ?? new Dictionary<Expression, Expression>();
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression parameter;
            if (this.parameterMap.TryGetValue(node, out parameter))
            {
                return parameter;
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            // re-perform any member-binding
            var expression = this.Visit(node.Expression);
            if (expression.Type != node.Type)
            {
                var newMember = expression.Type
                    .GetMember(node.Member.Name)
                    .Single();

                return Expression.MakeMemberAccess(expression, newMember);
            }

            return base.VisitMember(node);
        }
    }
}
