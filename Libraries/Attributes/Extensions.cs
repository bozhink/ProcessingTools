namespace ProcessingTools.Attributes
{
    using System.Linq;
    using System.Reflection;

    public static class Extensions
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
        public static string GetValue<T>(this T item)
            where T : struct
        {
            return item.GetType()
                .GetMember(item.ToString())
                .SelectMany(m => m.GetCustomAttributes<ValueAttribute>(false), (m, v) => v.Value)
                .FirstOrDefault() ?? item.ToString();
        }

        public static string GetDescription(this object item)
        {
            return item.GetType().GetCustomAttribute<DescriptionAttribute>(false)?.Description;
        }
    }
}