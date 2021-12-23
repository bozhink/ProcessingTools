// <copyright file="PrivateField.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Private field.
    /// </summary>
    public static class PrivateField
    {
        /// <summary>
        /// Gets a specified instance field.
        /// </summary>
        /// <typeparam name="T">Type of the object instance.</typeparam>
        /// <param name="instance">Object instance.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Value of the instance field.</returns>
        public static object GetInstanceField<T>(object instance, string fieldName)
        {
            return GetInstanceField(typeof(T), instance, fieldName);
        }

        /// <summary>
        /// Gets a specified instance field.
        /// </summary>
        /// <param name="type">Type of the object instance.</param>
        /// <param name="instance">Object instance.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>Value of the instance field.</returns>
        public static object GetInstanceField(Type type, object instance, string fieldName)
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

            var fieldInfo = type.GetField(fieldName, bindFlags);
            return fieldInfo?.GetValue(instance);
        }
    }
}
