// <copyright file="CommonBindingsModule.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Ninject.Modules
{
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Modules;
    using ProcessingTools.Constants;

    /// <summary>
    /// Common Bindings Module
    /// </summary>
    public class CommonBindingsModule : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            this.Bind(configure =>
            {
                configure.FromAssembliesMatching($"{nameof(ProcessingTools)}.*.{FileConstants.DllFileExtension}")
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
