// <copyright file="PrivateProperty.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Private property.
    /// </summary>
    public static class PrivateProperty
    {
        /// <summary>
        /// Gets a specified instance property.
        /// </summary>
        /// <typeparam name="T">Type of the object instance.</typeparam>
        /// <param name="instance">Object instance.</param>
        /// <param name="fieldName">Name of the property.</param>
        /// <returns>Value of the instance property.</returns>
        public static object GetInstanceProperty<T>(object instance, string fieldName)
        {
            return GetInstanceProperty(typeof(T), instance, fieldName);
        }

        /// <summary>
        /// Gets a specified instance property.
        /// </summary>
        /// <param name="type">Type of the object instance.</param>
        /// <param name="instance">Object instance.</param>
        /// <param name="fieldName">Name of the property.</param>
        /// <returns>Value of the instance property.</returns>
        public static object GetInstanceProperty(Type type, object instance, string fieldName)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            var bindFlags = BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static;

            var propertyInfo = type.GetProperty(fieldName, bindFlags);
            return propertyInfo?.GetValue(instance);
        }
    }
}
