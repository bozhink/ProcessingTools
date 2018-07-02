// <copyright file="EnumExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Enum Extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get enum value text pairs.
        /// </summary>
        /// <param name="enumType">Enum type.</param>
        /// <returns>IEnumerable of Value-Text pairs.</returns>
        public static IEnumerable GetEnumValueTextPairs(this Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException(nameof(enumType) + " should be of type System.Enum");
            }

            var values = Enum.GetValues(enumType).Cast<int>().OrderBy(v => v);

            var result = values.Select(v => new
            {
                Value = v,
                Text = Enum.Parse(enumType, v.ToString()).ToString()
            });

            return result.ToArray();
        }
    }
}
