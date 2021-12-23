// <copyright file="NinjectConfig.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Settings
{
    using System.Reflection;
    using global::Ninject;

    /// <summary>
    /// Ninject configuration.
    /// </summary>
    public static class NinjectConfig
    {
        /// <summary>
        /// Create instance of <see cref="IKernel"/>.
        /// </summary>
        /// <returns>Configured instnce of <see cref="IKernel"/>.</returns>
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Load(Assembly.GetExecutingAssembly());
            }
            finally
            {
                kernel.Dispose();
            }

            return kernel;
        }
    }
}