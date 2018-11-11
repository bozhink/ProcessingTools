// <copyright file="TableMapper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Serialization.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Mappings of CSV to object.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/3497699/csv-to-object-model-mapping.
    /// See http://stackoverflow.com/questions/9834061/c-sharp-user-defined-csv-mapping-to-poco.
    /// </remarks>
    public class TableMapper
    {
        /// <summary>
        /// Map table of strings to list of objects.
        /// </summary>
        /// <typeparam name="T">Type of the mapped object.</typeparam>
        /// <param name="table">Table of string values.</param>
        /// <param name="propertiesMapping">Column-to-property mapping.</param>
        /// <returns>List of mapped object.s</returns>
        public IEnumerable<T> MapTableToObjects<T>(IEnumerable<string[]> table, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            if (table == null || table.Any())
            {
                return new List<T> { };
            }

            var result = new List<T>(table.Count());

            foreach (string[] row in table)
            {
                result.Add(row.MapToObjectProperties<T>(propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }

        /// <summary>
        /// Map table of strings to list of objects.
        /// </summary>
        /// <param name="type">Type of the mapped object.</param>
        /// <param name="table">Table of string values.</param>
        /// <param name="propertiesMapping">Column-to-property mapping.</param>
        /// <returns>List of mapped object.s</returns>
        public IEnumerable<object> MapTableToObjects(Type type, IEnumerable<string[]> table, ColumnIndexToPropertyNameMapping propertiesMapping)
        {
            if (table == null || table.Any())
            {
                return new List<object> { };
            }

            var result = new List<object>(table.Count());

            foreach (string[] row in table)
            {
                result.Add(row.MapToObjectProperties(type, propertiesMapping));
            }

            result.TrimExcess();
            return result;
        }
    }
}
