// <copyright file="ConvertExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Convert extensions.
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Converts string to type.
        /// </summary>
        /// <typeparam name="T">Type of the output object.</typeparam>
        /// <param name="source">Source string.</param>
        /// <returns>Converted object.</returns>
        public static T ConvertTo<T>(this string source)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromString(source);
        }

        /// <summary>
        /// Converts string to type.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="type"><see cref="Type"/> of the output object.</param>
        /// <returns>Converted object.</returns>
        public static object ConvertTo(this string source, Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (converter == null)
            {
                return type.Default();
            }

            return converter.ConvertFromString(source);
        }
    }
}
