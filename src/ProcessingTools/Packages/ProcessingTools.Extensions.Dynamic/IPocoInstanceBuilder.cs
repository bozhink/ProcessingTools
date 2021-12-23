// <copyright file="IPocoInstanceBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System.Collections.Generic;

    /// <summary>
    /// Builder for POCO instances of specified type.
    /// </summary>
    public interface IPocoInstanceBuilder
    {
        /// <summary>
        /// Create instance of the specified type.
        /// </summary>
        /// <returns>Instance of the specified type.</returns>
        object CreateInstance();

        /// <summary>
        /// Create instance with specified values for properties.
        /// </summary>
        /// <param name="propertyValues">Values for properties.</param>
        /// <returns>Instance of the specified type with populated values.</returns>
        object CreateInstance(IDictionary<string, object> propertyValues);

        /// <summary>
        /// Gets the value of specified property of instance.
        /// </summary>
        /// <param name="instance">Instance of the type to be checked.</param>
        /// <param name="propertyName">Name of the property to get value.</param>
        /// <returns>Value for the property.</returns>
        /// <exception cref="System.ArgumentNullException">If instance is null or property name is null or empty.</exception>
        object GetValue(object instance, string propertyName);

        /// <summary>
        /// Sets value of property for specified instance.
        /// </summary>
        /// <param name="instance">Instance of the type to be updated.</param>
        /// <param name="propertyName">Name of the property to set value.</param>
        /// <param name="propertyValue">Value for the property to be set.</param>
        /// <exception cref="System.ArgumentNullException">If instance is null or property name is null or empty.</exception>
        void SetValue(object instance, string propertyName, object propertyValue);
    }
}
