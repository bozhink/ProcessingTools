// <copyright file="MappingsExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Linq;

    /// <summary>
    /// Mappings extensions.
    /// </summary>
    public static class MappingsExtensions
    {
        /// <summary>
        /// Map object to type.
        /// </summary>
        /// <typeparam name="T">Type of the output object.</typeparam>
        /// <param name="item">Object to be mapped.</param>
        /// <returns>resultant object.</returns>
        public static T Map<T>(this object item)
            where T : new()
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            T result = Activator.CreateInstance<T>();

            var itemProperties = item.GetType().GetProperties();
            var resultProperties = typeof(T).GetProperties();
            foreach (var itemProperty in itemProperties)
            {
                var resultProperty = resultProperties
                    .FirstOrDefault(p => p.Name == itemProperty.Name && p.PropertyType == itemProperty.PropertyType);

                resultProperty?.SetValue(result, itemProperty.GetValue(item));
            }

            return result;
        }
    }
}
