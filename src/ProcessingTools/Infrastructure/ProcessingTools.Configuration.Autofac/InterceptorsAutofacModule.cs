// <copyright file="InterceptorsAutofacModule.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using global::Autofac;
    using ProcessingTools.Configuration.Interceptors;

    /// <summary>
    /// Interceptors module.
    /// </summary>
    public class InterceptorsAutofacModule : Module
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
