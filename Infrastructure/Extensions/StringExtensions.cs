namespace ProcessingTools.Extensions
{
    using System.ComponentModel;

    public static class StringExtensions
    {
        public static T Convert<T>(this string input)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromString(input);
        }
    }
}