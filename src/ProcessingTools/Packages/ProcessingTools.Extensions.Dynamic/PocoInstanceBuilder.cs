// <copyright file="PocoInstanceBuilder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Builder for POCO instances of specified type.
    /// </summary>
    public class PocoInstanceBuilder : IPocoInstanceBuilder
    {
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoInstanceBuilder"/> class.
        /// </summary>
        /// <param name="type">Type of instance.</param>
        public PocoInstanceBuilder(Type type)
        {
            this.type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <inheritdoc/>
        public object CreateInstance()
        {
            return Activator.CreateInstance(this.type);
        }

        /// <inheritdoc/>
        public object CreateInstance(IDictionary<string, object> propertyValues)
        {
            var instance = this.CreateInstance();

            if (propertyValues != null && propertyValues.Any())
            {
                foreach (var propertyValue in propertyValues)
                {
                    this.SetValue(instance, propertyValue.Key, propertyValue.Value);
                }
            }

            return instance;
        }

        /// <inheritdoc/>
        public object GetValue(object instance, string propertyName)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            return this.type.InvokeMember(propertyName, BindingFlags.GetProperty, null, instance, Array.Empty<object>(), CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public void SetValue(object instance, string propertyName, object propertyValue)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            this.type.InvokeMember(propertyName, BindingFlags.SetProperty, null, instance, new[] { propertyValue }, CultureInfo.InvariantCulture);
        }
    }
}
