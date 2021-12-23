// <copyright file="DynamicProperty.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;

    /// <summary>
    /// Dynamic property.
    /// </summary>
    public class DynamicProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicProperty"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        public DynamicProperty(string name, Type type)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public Type Type { get; }
    }
}
