// <copyright file="PocoTypeBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Dynamic builder for POCO types.
    /// </summary>
    public class PocoTypeBuilder : IPocoTypeBuilder
    {
        private readonly TypeBuilder typeBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoTypeBuilder"/> class.
        /// </summary>
        /// <param name="moduleBuilder">Instance of <see cref="ModuleBuilder"/>.</param>
        public PocoTypeBuilder(ModuleBuilder moduleBuilder)
            : this(moduleBuilder, Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoTypeBuilder"/> class.
        /// </summary>
        /// <param name="moduleBuilder">Instance of <see cref="ModuleBuilder"/>.</param>
        /// <param name="typeName">Name of the resultant type.</param>
        public PocoTypeBuilder(ModuleBuilder moduleBuilder, string typeName)
        {
            if (moduleBuilder is null)
            {
                throw new ArgumentNullException(nameof(moduleBuilder));
            }

            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException(nameof(typeName));
            }

            this.typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public);
        }

        /// <inheritdoc/>
        public void AddProperty(string propertyName, Type propertyType)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (propertyType is null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            this.typeBuilder.AddProperty(propertyName, propertyType);
        }

        /// <inheritdoc/>
        public Type CreateType()
        {
            return this.typeBuilder.CreateType();
        }
    }
}
