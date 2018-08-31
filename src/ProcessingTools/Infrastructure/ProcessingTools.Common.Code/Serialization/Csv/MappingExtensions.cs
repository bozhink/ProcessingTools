// <copyright file="MappingExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Serialization.Csv
{
    using System;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Mapping extensions.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Map values to object properties.
        /// </summary>
        /// <typeparam name="T">Type of mapped object.</typeparam>
        /// <param name="values">Array of string values to be mapped.</param>
        /// <param name="propertiesMapping">Column-to-property mapping.</param>
        /// <returns>Mapped object.</returns>
        public static T MapToObjectProperties<T>(this string[] values, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            return (T)values.MapToObjectProperties(typeof(T), propertiesMapping);
        }

        /// <summary>
        /// Map values to object properties.
        /// </summary>
        /// <param name="values">Array of string values to be mapped.</param>
        /// <param name="type">Type of mapped object.</param>
        /// <param name="propertiesMapping">Column-to-property mapping.</param>
        /// <returns>Mapped object.</returns>
        public static object MapToObjectProperties(this string[] values, Type type, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertiesMapping == null)
            {
                throw new ArgumentNullException(nameof(propertiesMapping));
            }

            if (values == null || values.Length < 1)
            {
                return type.Default();
            }

            int numberOfItems = values.Length;

            var result = Activator.CreateInstance(type);

            foreach (var property in type.GetProperties())
            {
                if (propertiesMapping.Mapping.Keys.Contains(property.Name))
                {
                    int index = propertiesMapping.Mapping[property.Name];
                    if (numberOfItems <= index || index < 0)
                    {
                        throw new InvalidColumnMappingException();
                    }

                    var propertyType = property.PropertyType;
                    try
                    {
                        object value = values[index];
                        if (propertyType != typeof(string))
                        {
                            value = values[index].ConvertTo(propertyType);
                        }

                        property.SetValue(result, value, null);
                    }
                    catch (Exception e)
                    {
                        throw new NotSupportedException($"Cannot convert from string to {propertyType}.", e);
                    }
                }
            }

            return result;
        }
    }
}
