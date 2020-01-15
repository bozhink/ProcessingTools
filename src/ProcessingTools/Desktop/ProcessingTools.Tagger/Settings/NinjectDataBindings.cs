// <copyright file="NinjectDataBindings.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind database objects.
    /// </summary>
    public class NinjectDataBindings : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            this.Bind(b =>
            {
            });
        }
    }
}
