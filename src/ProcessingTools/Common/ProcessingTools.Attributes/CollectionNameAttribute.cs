// <copyright file="CollectionNameAttribute.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Attributes
{
    using System;

    /// <summary>
    /// Collection Name Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionNameAttribute"/> class.
        /// </summary>
        /// <param name="name">Name of the collection.</param>
        public CollectionNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        public string Name { get; }
    }
}
