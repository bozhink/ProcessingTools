﻿// <copyright file="DynamicProperty.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    using System;

    /// <summary>
    /// Dynamic property.
    /// </summary>
    public class DynamicProperty
    {
        private readonly string name;
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicProperty"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="type">The type of the property.</param>
        public DynamicProperty(string name, Type type)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name => this.name;

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        public Type Type => this.type;
    }
}
