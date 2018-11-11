// <copyright file="FileNameAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Attributes
{
    using System;

    /// <summary>
    /// File name attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FileNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        public FileNameAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string Name { get; }
    }
}
