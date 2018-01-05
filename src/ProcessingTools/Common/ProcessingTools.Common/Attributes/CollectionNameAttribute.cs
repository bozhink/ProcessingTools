// <copyright file="CollectionNameAttribute.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Attributes
{
    using System;

    /// <summary>
    /// Collection Name Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CollectionNameAttribute : Attribute
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionNameAttribute"/> class.
        /// </summary>
        /// <param name="name">Name of the collection.</param>
        public CollectionNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.name = value;
            }
        }
    }
}
