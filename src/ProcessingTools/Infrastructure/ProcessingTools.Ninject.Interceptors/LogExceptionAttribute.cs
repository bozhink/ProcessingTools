// <copyright file="LogExceptionAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

// See https://blog.kulman.sk/intercepting-methods-with-ninject-for-error-logging/
namespace ProcessingTools.Ninject.Interceptors
{
    using System;
    using global::Ninject;
    using global::Ninject.Extensions.Interception;
    using global::Ninject.Extensions.Interception.Attributes;
    using global::Ninject.Extensions.Interception.Request;

    /// <summary>
    /// Log exception attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class LogExceptionAttribute : InterceptAttribute
    {
        /// <inheritdoc/>
        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            return request.Context.Kernel.Get<ExceptionLoggingInterceptor>();
        }
    }
}
