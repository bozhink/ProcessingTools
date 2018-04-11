// <copyright file="InterceptorsModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Settings
{
    using Autofac;
    using ProcessingTools.Web.Documents.Interceptors;

    /// <summary>
    /// Interceptors module.
    /// </summary>
    public class InterceptorsModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var x = c.Resolve<IComponentContext>();
                return new FactoryInterceptor((name, type) => x.ResolveNamed(name, type));
            });
        }
    }
}
