// <copyright file="NinjectConfig.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Settings
{
    using System.Reflection;
    using global::Ninject;

    /// <summary>
    /// Ninject configuration.
    /// </summary>
    internal static class NinjectConfig
    {
        /// <summary>
        /// Create new instance of <see cref="IKernel"/>.
        /// </summary>
        /// <returns>Instance of <see cref="IKernel"/>.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());
            }
            catch
            {
                kernel.Dispose();
                throw;
            }

            return kernel;
        }
    }
}
