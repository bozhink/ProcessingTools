// <copyright file="NinjectConfig.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Settings
{
    using System.Reflection;
    using global::Ninject;

    internal static class NinjectConfig
    {
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
