// <copyright file="StringExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// Convert extensions.
    /// </summary>
    public static class StringExtensions
    {
        private const string ControllerNameSuffix = "Controller";

        /// <summary>
        /// Get controller name.
        /// </summary>
        /// <typeparam name="T">Type of the controller.</typeparam>
        /// <returns>Name of the controller.</returns>
        public static string GetControllerName<T>()
        {
            var type = typeof(T);
            string name = type.Name;
            int suffixIndex = name.IndexOf(value: ControllerNameSuffix, startIndex: 0, comparisonType: StringComparison.InvariantCulture);

            if (suffixIndex > 0)
            {
                return name.Substring(0, suffixIndex);
            }

            return name;
        }

        /// <summary>
        /// Get description message for command.
        /// </summary>
        /// <param name="type">Source type.</param>
        /// <returns>Description as string.</returns>
        public static string GetDescriptionMessageForCommand(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string message = type.GetCustomAttribute<DescriptionAttribute>(false)?.Description;

            if (string.IsNullOrWhiteSpace(message))
            {
                var name = Regex.Replace(type.FullName, @".*?([^\.]+)\Z", "$1");
                name = Regex.Replace(name, @"Command\Z", string.Empty);

                message = Regex.Replace(name, "(?=[A-Z])", " ").Trim();
            }

            return message;
        }

        /// <summary>
        /// Converts string to type.
        /// </summary>
        /// <typeparam name="T">Type of the output object.</typeparam>
        /// <param name="source">Source string.</param>
        /// <returns>Converted object.</returns>
        public static T ConvertTo<T>(this string source)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter is null)
            {
                return default;
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
            if (converter is null)
            {
                return type.Default();
            }

            return converter.ConvertFromString(source);
        }

        /// <summary>
        /// Parse string to <see cref="Guid"/> or generates new <see cref="Guid"/> if parse is invalid.
        /// </summary>
        /// <param name="source">Source string to be parsed.</param>
        /// <returns>Parsed or new <see cref="Guid"/>.</returns>
        public static Guid ToNewGuid(this string source)
        {
            if (!string.IsNullOrWhiteSpace(source) && Guid.TryParse(source, out Guid result))
            {
                return result;
            }

            return Guid.NewGuid();
        }

        /// <summary>
        /// Parse object to <see cref="Guid"/> or generates new <see cref="Guid"/> if parse is invalid.
        /// </summary>
        /// <param name="source">Source object to be parsed.</param>
        /// <returns>Parsed or new <see cref="Guid"/>.</returns>
        public static Guid ToNewGuid(this object source)
        {
            return (source?.ToString()).ToNewGuid();
        }

        /// <summary>
        /// Parse string to <see cref="Guid"/> or returns empty <see cref="Guid"/> if parse is invalid.
        /// </summary>
        /// <param name="source">Source string to be parsed.</param>
        /// <returns>Parsed or empty <see cref="Guid"/>.</returns>
        public static Guid ToEmptyGuid(this string source)
        {
            if (!string.IsNullOrWhiteSpace(source) && Guid.TryParse(source, out Guid result))
            {
                return result;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Parse object to <see cref="Guid"/> or returns empty <see cref="Guid"/> if parse is invalid.
        /// </summary>
        /// <param name="source">Source object to be parsed.</param>
        /// <returns>Parsed or empty <see cref="Guid"/>.</returns>
        public static Guid ToEmptyGuid(this object source)
        {
            return (source?.ToString()).ToEmptyGuid();
        }
    }
}
