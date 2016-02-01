namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using ProcessingTools.Infrastructure.Extensions;

    /// <summary>
    /// Mappings of CSV to object.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/3497699/csv-to-object-model-mapping.
    /// See http://stackoverflow.com/questions/9834061/c-sharp-user-defined-csv-mapping-to-poco.
    /// </remarks>
    public class CsvMapper
    {
        public IEnumerable<T> MapCsvTableToObjects<T>(string[][] csvTable, CsvToObjectMapping propertiesMapping)
        {
            var result = new List<T>();

            foreach (string[] csvRow in csvTable)
            {
                result.Add(this.MapCsvRowToObject<T>(csvRow, propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }

        public IEnumerable<object> MapCsvTableToObjects(Type type, string[][] csvTable, CsvToObjectMapping propertiesMapping)
        {
            var result = new List<object>();

            foreach (string[] csvRow in csvTable)
            {
                result.Add(this.MapCsvRowToObject(type, csvRow, propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }

        public T MapCsvRowToObject<T>(string[] csvRowValues, CsvToObjectMapping propertiesMapping)
        {
            return (T)this.MapCsvRowToObject(typeof(T), csvRowValues, propertiesMapping);
        }

        public object MapCsvRowToObject(Type type, string[] csvRowValues, CsvToObjectMapping propertiesMapping)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (propertiesMapping == null)
            {
                throw new ArgumentNullException("propertiesMapping");
            }

            var result = Activator.CreateInstance(type);

            IList<PropertyInfo> properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (propertiesMapping.Mapping.Keys.Contains(property.Name))
                {
                    var propertyType = property.PropertyType;

                    if (propertyType == typeof(string))
                    {
                        var value = csvRowValues[propertiesMapping.Mapping[property.Name]];
                        property.SetValue(result, value, null);
                    }
                    else
                    {
                        try
                        {
                            var value = csvRowValues[propertiesMapping.Mapping[property.Name]].ConvertTo(propertyType);
                            property.SetValue(result, value, null);
                        }
                        catch (Exception e)
                        {
                            throw new NotSupportedException($"Cannot convert from string to {propertyType}", e);
                        }
                    }
                }
            }

            return result;
        }
    }
}