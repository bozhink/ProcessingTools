// <copyright file="GenericExpressionVisitor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Generic expression visitor.
    /// </summary>
    /// <typeparam name="T1">T1.</typeparam>
    /// <typeparam name="T2">T2.</typeparam>
    /// <remarks>
    /// See http://stackoverflow.com/questions/11248585/how-to-map-two-expressions-of-differing-types.
    /// </remarks>
    internal class GenericExpressionVisitor<T1, T2> : ExpressionVisitor
    {
        private readonly Type typeofT1;
        private readonly Type typeofT2;
        private readonly IEnumerable<PropertyInfo> typeofBProperties;

        private readonly Stack<ParameterExpression[]> parameterStack;
        private readonly IDictionary<string, string> propertyNamesMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericExpressionVisitor{S, B}"/> class.
        /// </summary>
        public GenericExpressionVisitor()
        {
            this.typeofT1 = typeof(T1);
            this.typeofT2 = typeof(T2);
            this.typeofBProperties = this.typeofT2.GetProperties();

            this.parameterStack = new Stack<ParameterExpression[]>();

            this.propertyNamesMap = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericExpressionVisitor{S, B}"/> class.
        /// </summary>
        /// <param name="propertyNameMap">Property name map.</param>
        public GenericExpressionVisitor(IDictionary<string, string> propertyNameMap)
        {
            this.typeofT1 = typeof(T1);
            this.typeofT2 = typeof(T2);
            this.typeofBProperties = this.typeofT2.GetProperties();

            this.parameterStack = new Stack<ParameterExpression[]>();

            this.propertyNamesMap = propertyNameMap;
        }

        /// <inheritdoc/>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var lambda = (LambdaExpression)node;
            var parameters = lambda.Parameters
                .Select(p => this.typeofT1 == p.Type ? Expression.Parameter(this.typeofT2) : p)
                .ToArray();

            this.parameterStack.Push(parameters);

            lambda = Expression.Lambda(this.Visit(lambda.Body), this.parameterStack.Pop());

            return lambda;
        }

        /// <inheritdoc/>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var memberExpression = node;
            var member = memberExpression.Member;
            var name = member.Name;

            if (member.MemberType == MemberTypes.Property &&
                (this.typeofT1 == member.DeclaringType || this.typeofT1.GetInterfaces().Contains(member.DeclaringType)))
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

        /// <inheritdoc/>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            node = (ParameterExpression)base.VisitParameter(node);

            if (this.typeofT1 == node.Type)
            {
                node = this.parameterStack.Peek().Single(p => p.Type == this.typeofT2);
            }

            return node;
        }
    }
}
