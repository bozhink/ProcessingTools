// <copyright file="AttributeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Attribute extensions.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Gets the value of the on an struct, including enums.
        /// </summary>
        /// <typeparam name="T">The type of the struct.</typeparam>
        /// <param name="item">A value of type <see cref="T:System.Enum"/>.</param>
        /// <returns>If the struct has a Value attribute, this method returns the description.
        /// Otherwise it just calls ToString() on the struct.</returns>
        /// <remarks>Based on http://stackoverflow.com/questions/479410/enum-tostring/479417#479417 and
        /// http://stackoverflow.com/questions/479410/enum-tostring-with-user-friendly-strings,
        /// but useful for any struct.</remarks>
        public static string GetName<T>(this T item)
            where T : struct
        {
            return item.GetType()
                .GetMember(item.ToString())
                .SelectMany(m => m.GetCustomAttributes<DisplayAttribute>(false), (m, v) => v.Name)
                .FirstOrDefault() ?? item.ToString();
        }

        /// <summary>
        /// Gets the Description of <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="item">Object to be harvested.</param>
        /// <returns>Value of the Description property of <see cref="DescriptionAttribute" />.</returns>
        public static string GetDescription(this object item)
        {
            return item.GetType().GetCustomAttribute<DescriptionAttribute>(false)?.Description;
        }
    }
}
