// <copyright file="IPocoTypeBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;

    /// <summary>
    /// Dynamic builder for POCO types.
    /// </summary>
    public interface IPocoTypeBuilder
    {
        /// <summary>
        /// Add property with public getter and setter of specified type with specified name to the build type.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyType">Type of the property.</param>
        /// <exception cref="System.ArgumentNullException">If the property name id null or empty or the property type is null.</exception>
        void AddProperty(string propertyName, Type propertyType);

        /// <summary>
        /// Create the resultant type.
        /// </summary>
        /// <returns>Resultant type.</returns>
        Type CreateType();
    }
}
