namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections;
    using System.Linq;

    public static class EnumExtensions
    {
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
