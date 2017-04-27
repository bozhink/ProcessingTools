namespace ProcessingTools.Extensions
{
    using System;
    using System.Linq;

    public static class MappingsExtensions
    {
        public static T Map<T>(this object item)
            where T : class
        {
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