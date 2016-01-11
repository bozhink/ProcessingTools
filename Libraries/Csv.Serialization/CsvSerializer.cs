namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class CsvSerializer
    {
        private readonly int numberOfRowsToSkip;
        private readonly CsvObjectConfiguration configuration;

        public CsvSerializer()
            : this(new CsvObjectConfiguration())
        {
        }

        public CsvSerializer(CsvObjectConfiguration configuration)
        {
            this.configuration = configuration;
            this.numberOfRowsToSkip = this.configuration.FirstRow < 1 ? 0 : this.configuration.FirstRow;
        }

        public IEnumerable<T> Deserialize<T>(string csvText)
        {
            Type type = typeof(T);
            if (!Attribute.IsDefined(type, typeof(CsvObjectAttribute)))
            {
                throw new ArgumentException("Type schould contain CsvObjectAttribute");
            }

            var table = this.CsvToTable(csvText);
            var mappings = this.CreatePropertiesMapping(type, table);
            var data = this.GetDataPartOfTheCsv(table);

            var mapper = new CsvMapper();
            return mapper.MapCsvTableToObjects<T>(data, mappings);
        }

        public IEnumerable<object> Deserialize(Type type, string csvText)
        {
            if (!Attribute.IsDefined(type, typeof(CsvObjectAttribute)))
            {
                throw new ArgumentException("Type schould contain CsvObjectAttribute", "type");
            }

            var table = this.CsvToTable(csvText);
            var mappings = this.CreatePropertiesMapping(type, table);
            var data = this.GetDataPartOfTheCsv(table);

            var mapper = new CsvMapper();
            return mapper.MapCsvTableToObjects(type, data, mappings);
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

        private CsvToObjectMapping CreatePropertiesMapping(Type type, string[][] table)
        {
            if (this.numberOfRowsToSkip < 1)
            {
                throw new ApplicationException("This CSV is not supposed to have header row. Current method is not applicable in this case.");
            }

            var mappings = new CsvToObjectMapping();

            var headers = table[0];
            int headersLength = headers.Length;

            var properties = type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(CsvColumnAttribute)));

            foreach (var property in properties)
            {
                string columnAttributeName = property.GetCustomAttribute<CsvColumnAttribute>(false)?.Name;
                string columnName = string.IsNullOrWhiteSpace(columnAttributeName) ? property.Name : columnAttributeName;

                int columnNumber = -1;
                for (int i = 0; i < headersLength; ++i)
                {
                    if (string.Compare(headers[i], columnName) == 0)
                    {
                        columnNumber = i;
                        break;
                    }
                }

                if (columnNumber >= 0)
                {
                    mappings.Mapping.Add(columnName, columnNumber);
                }
            }

            return mappings;
        }
    }
}