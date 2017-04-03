/// <summary>
/// See http://stackoverflow.com/questions/11248585/how-to-map-two-expressions-of-differing-types
/// </summary>
namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class GenericExpressionVisitor<S, B> : ExpressionVisitor
    {
        private readonly Type typeofS;
        private readonly IEnumerable<Type> typeofSInterfaces;
        private readonly Type typeofB;
        private readonly IEnumerable<PropertyInfo> typeofBProperties;

        private readonly Stack<ParameterExpression[]> parameterStack;
        private readonly IDictionary<string, string> propertyNamesMap;


        public GenericExpressionVisitor()
        {
            this.typeofS = typeof(S);
            this.typeofSInterfaces = typeofS.GetInterfaces();

            this.typeofB = typeof(B);
            this.typeofBProperties = typeofB.GetProperties();

            this.parameterStack = new Stack<ParameterExpression[]>();

            this.propertyNamesMap = null;
        }

        public GenericExpressionVisitor(IDictionary<string, string> propertyNamesMap)
        {
            this.typeofS = typeof(S);
            this.typeofSInterfaces = typeofS.GetInterfaces();

            this.typeofB = typeof(B);
            this.typeofBProperties = typeofB.GetProperties();

            this.parameterStack = new Stack<ParameterExpression[]>();

            this.propertyNamesMap = propertyNamesMap;
        }

        ////public override Expression Visit(Expression node)
        ////{
        ////    return base.Visit(node);
        ////}

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var lambda = (LambdaExpression)node;
            var parameters = lambda.Parameters
                .Select(p => this.typeofS == p.Type ? Expression.Parameter(this.typeofB) : p)
                .ToArray();

            this.parameterStack.Push(parameters);

            lambda = Expression.Lambda(this.Visit(lambda.Body), this.parameterStack.Pop());

            return lambda;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var memberExpression = (MemberExpression)node;
            var member = memberExpression.Member;
            var name = member.Name;

            if (member.MemberType == MemberTypes.Property &&
                (this.typeofS == member.DeclaringType || this.typeofS.GetInterfaces().Contains(member.DeclaringType)))
            {
                string propertyName = name;

                if (this.propertyNamesMap != null && this.propertyNamesMap.ContainsKey(propertyName))
                {
                    propertyName = this.propertyNamesMap[propertyName];
                }

                memberExpression = Expression.Property(
                    this.Visit(memberExpression.Expression),
                    this.typeofBProperties.First(p => p.Name == propertyName));
            }

            return memberExpression;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            node = (ParameterExpression)base.VisitParameter(node);

            if (this.typeofS == node.Type)
            {
                node = this.parameterStack.Peek().Single(p => p.Type == this.typeofB);
            }

            return node;
        }
    }
}
