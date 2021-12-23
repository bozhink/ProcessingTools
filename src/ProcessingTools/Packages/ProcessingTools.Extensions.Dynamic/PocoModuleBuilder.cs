// <copyright file="PocoModuleBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// <see cref="System.Reflection.Emit.ModuleBuilder"/> factory for dynamic POCO objects.
    /// </summary>
    public class PocoModuleBuilder : IPocoModuleBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PocoModuleBuilder"/> class with default assembly name.
        /// </summary>
        public PocoModuleBuilder()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoModuleBuilder"/> class with specified assembly name.
        /// </summary>
        /// <param name="name">Name of the assembly.</param>
        public PocoModuleBuilder(string name)
            : this(new AssemblyName(name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoModuleBuilder"/> class with specified assembly name.
        /// </summary>
        /// <param name="assemblyName">Assembly name instance.</param>
        public PocoModuleBuilder(AssemblyName assemblyName)
        {
            this.AssemblyName = assemblyName ?? throw new ArgumentNullException(nameof(assemblyName));
            this.AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(this.AssemblyName, AssemblyBuilderAccess.RunAndCollect);
            this.ModuleBuilder = this.AssemblyBuilder.DefineDynamicModule(this.AssemblyName.Name);
        }

        /// <inheritdoc/>
        public AssemblyName AssemblyName { get; }

        /// <inheritdoc/>
        public AssemblyBuilder AssemblyBuilder { get; }

        /// <inheritdoc/>
        public ModuleBuilder ModuleBuilder { get; }
    }
}
