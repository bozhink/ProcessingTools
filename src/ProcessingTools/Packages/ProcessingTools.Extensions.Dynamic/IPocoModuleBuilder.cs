// <copyright file="IPocoModuleBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// <see cref="System.Reflection.Emit.ModuleBuilder"/> factory for dynamic POCO objects.
    /// </summary>
    public interface IPocoModuleBuilder
    {
        /// <summary>
        /// Gets the initialized instance of <see cref="System.Reflection.AssemblyName"/>.
        /// </summary>
        AssemblyBuilder AssemblyBuilder { get; }

        /// <summary>
        /// Gets the initialized instance of <see cref="System.Reflection.Emit.AssemblyBuilder"/>.
        /// </summary>
        AssemblyName AssemblyName { get; }

        /// <summary>
        /// Gets the initialized instance of <see cref="System.Reflection.Emit.ModuleBuilder"/>.
        /// </summary>
        ModuleBuilder ModuleBuilder { get; }
    }
}
