namespace ProcessingTools.Infrastructure.Serialization.Csv
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Mappings of CSV to object.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/3497699/csv-to-object-model-mapping.
    /// See http://stackoverflow.com/questions/9834061/c-sharp-user-defined-csv-mapping-to-poco.
    /// </remarks>
    public class TableMapper
    {
        public IEnumerable<T> MapTableToObjects<T>(string[][] table, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            var result = new List<T>();

            foreach (string[] row in table)
            {
                result.Add(row.MapToObjectProperties<T>(propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }

        public IEnumerable<object> MapTableToObjects(Type type, string[][] table, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            var result = new List<object>();

            foreach (string[] row in table)
            {
                result.Add(row.MapToObjectProperties(type, propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }
    }
}