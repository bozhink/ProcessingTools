namespace ProcessingTools.Extensions
{
    using System;
    using System.ComponentModel;

    public static class ConvertExtensions
    {
        public static T ConvertTo<T>(this string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromString(input);
        }

        public static object ConvertTo(this string input, Type type)
        {
            var converter = TypeDescriptor.GetConverter(type);
            if (converter == null)
            {
                return type.Default();
            }

            return converter.ConvertFromString(input);
        }
    }
}
