// <copyright file="FactoryInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Interceptors
{
    using System;
    using Castle.DynamicProxy;

    /// <summary>
    /// Factory interceptor.
    /// </summary>
    public class FactoryInterceptor : IInterceptor
    {
        private readonly Func<string, Type, object> objectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryInterceptor"/> class.
        /// </summary>
        /// <param name="objectFactory">Object factory.</param>
        public FactoryInterceptor(Func<string, Type, object> objectFactory)
        {
            this.objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            var o = this.objectFactory.Invoke(invocation.Method.Name, invocation.Method.ReturnType);
            invocation.ReturnValue = o;
        }
    }
}
