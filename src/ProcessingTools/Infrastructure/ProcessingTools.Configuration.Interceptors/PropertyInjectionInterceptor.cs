// <copyright file="PropertyInjectionInterceptor.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Interceptors
{
    using System;
    using Castle.DynamicProxy;

    /// <summary>
    /// Property injection interceptor.
    /// </summary>
    public class PropertyInjectionInterceptor : IInterceptor
    {
        private readonly Func<Type, object> objectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyInjectionInterceptor"/> class.
        /// </summary>
        /// <param name="objectFactory">Object factory.</param>
        public PropertyInjectionInterceptor(Func<Type, object> objectFactory)
        {
            this.objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory));
        }

        /// <inheritdoc/>
        public void Intercept(IInvocation invocation)
        {
            // TODO
            var o = this.objectFactory.Invoke(invocation.Method.ReturnType);
            invocation.ReturnValue = o;
        }
    }
}
