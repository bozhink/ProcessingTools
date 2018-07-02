// <copyright file="InjectPropertyAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Attributes
{
    using System;

    /// <summary>
    /// Inject property attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class InjectPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectPropertyAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property to be injected.</param>
        public InjectPropertyAttribute(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the name of the property to be injected.
        /// </summary>
        public string PropertyName { get; }
    }
}
