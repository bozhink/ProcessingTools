namespace ProcessingTools.Common.Serialization.Csv
{
    using ProcessingTools.Extensions;
    using System;

    public static class MappingExtensions
    {
        public static T MapToObjectProperties<T>(this string[] values, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            return (T)values.MapToObjectProperties(typeof(T), propertiesMapping);
        }

        public static object MapToObjectProperties(this string[] values, Type type, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertiesMapping == null)
            {
                throw new ArgumentNullException(nameof(propertiesMapping));
            }

            if (values == null || values.Length < 1)
            {
                return type.Default();
            }

            int numberOfItems = values.Length;

            var result = Activator.CreateInstance(type);

            foreach (var property in type.GetProperties())
            {
                if (propertiesMapping.Mapping.Keys.Contains(property.Name))
                {
                    int index = propertiesMapping.Mapping[property.Name];
                    if (numberOfItems <= index || index < 0)
                    {
                        throw new IndexOutOfRangeException("Invalid mapping.");
                    }

                    var propertyType = property.PropertyType;
                    try
                    {
                        object value = values[index];
                        if (propertyType != typeof(string))
                        {
                            value = values[index].ConvertTo(propertyType);
                        }

                        property.SetValue(result, value, null);
                    }
                    catch (Exception e)
                    {
                        throw new NotSupportedException($"Cannot convert from string to {propertyType}.", e);
                    }
                }
            }

            return result;
        }
    }
}
