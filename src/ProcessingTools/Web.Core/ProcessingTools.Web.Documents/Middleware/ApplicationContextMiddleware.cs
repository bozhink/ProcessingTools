// <copyright file="ApplicationContextMiddleware.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Application context middleware.
    /// </summary>
    public class ApplicationContextMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContextMiddleware"/> class.
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/> callback.</param>
        public ApplicationContextMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invokes middleware.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> of execution.</param>
        /// <param name="factory"><see cref="ApplicationContextFactory"/> instance.</param>
        /// <returns>Task</returns>
        public Task Invoke(HttpContext context, ApplicationContextFactory factory)
        {
            if (factory != null)
            {
                factory.ApplicationContext = new ApplicationContext(context.User);
            }

            return this.next(context);
        }
    }
}
