// <copyright file="CsvSerializer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// CSV serializer.
    /// </summary>
    public class CsvSerializer
    {
        private readonly int numberOfRowsToSkip;
        private readonly CsvObjectConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvSerializer"/> class.
        /// </summary>
        public CsvSerializer()
            : this(new CsvObjectConfiguration())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvSerializer"/> class.
        /// </summary>
        /// <param name="configuration">The CSV object configuration.</param>
        public CsvSerializer(CsvObjectConfiguration configuration)
        {
            this.configuration = configuration;
            this.numberOfRowsToSkip = this.configuration.FirstRow < 1 ? 0 : this.configuration.FirstRow;
        }

        /// <summary>
        /// Deserializes CSV text to list of models.
        /// </summary>
        /// <typeparam name="T">Type of CSV serialization model.</typeparam>
        /// <param name="text">CSV as string.</param>
        /// <returns>Deserialized text.</returns>
        public IEnumerable<T> Deserialize<T>(string text)
        {
            return this.Deserialize(typeof(T), text).Cast<T>();
        }

        /// <summary>
        /// Deserializes CSV text to list of objects.
        /// </summary>
        /// <param name="type">Type of the serialization model.</param>
        /// <param name="text">CSV as string.</param>
        /// <returns>Deserialized text.</returns>
        public IEnumerable<object> Deserialize(Type type, string text)
        {
            if (!Attribute.IsDefined(type, typeof(CsvObjectAttribute)))
            {
                throw new ArgumentException("Type should contain CsvObjectAttribute", nameof(type));
            }

            var table = this.CsvToTable(text);
            var mappings = this.CreatePropertiesMapping(type, table);
            var data = this.GetDataPartOfTheCsv(table);

            var mapper = new TableMapper();
            return mapper.MapTableToObjects(type, data, mappings);
        }

        private string[][] GetDataPartOfTheCsv(string[][] table)
        {
            return table.Skip(this.numberOfRowsToSkip).ToArray();
        }

        private string[][] CsvToTable(string csvText)
        {
            var csvTableReader = new CsvTableReader(this.configuration);
            var table = csvTableReader.ReadToTable(csvText);
            return table;
        }

        private ColumnIndexToPropertyNameMapping CreatePropertiesMapping(Type type, string[][] table)
        {
            if (this.numberOfRowsToSkip < 1)
            {
                throw new InvalidOperationException("This CSV is not supposed to have header row. Current method is not applicable in this case.");
            }

            var mappings = new ColumnIndexToPropertyNameMapping();

            var header = table[0];
            int headerLength = header.Length;

            var properties = type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(CsvColumnAttribute)));

            foreach (var property in properties)
            {
                string columnName = this.GetColumnNameInTable(property);
                if (!string.IsNullOrEmpty(columnName))
                {
                    int columnNumber = -1;
                    for (int i = 0; i < headerLength; ++i)
                    {
                        if (string.Compare(header[i], columnName) == 0)
                        {
                            columnNumber = i;
                            break;
                        }
                    }

                    if (columnNumber >= 0)
                    {
                        mappings.Mapping.Add(property.Name, columnNumber);
                    }
                }
            }

            return mappings;
        }

        /// <summary>
        /// Gets the expected name of the column as it should appear in the table.
        /// </summary>
        /// <param name="property">The model’s property which provides information about the column name.</param>
        /// <returns>The expected name of the column as it should appear in the table.</returns>
        private string GetColumnNameInTable(PropertyInfo property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var attribute = property.GetCustomAttribute<CsvColumnAttribute>(false);
            if (attribute == null)
            {
                return null;
            }

            return string.IsNullOrWhiteSpace(attribute.Name) ? property.Name : attribute.Name;
        }
    }
}
