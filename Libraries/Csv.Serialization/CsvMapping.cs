namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Mappings of CSV to object.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/3497699/csv-to-object-model-mapping
    /// See http://stackoverflow.com/questions/9834061/c-sharp-user-defined-csv-mapping-to-poco
    /// </remarks>
    public class CsvMapping
    {
        // use reflection to parse the CSV survey input
        public bool ParseCSV(string csvString, CSVMapping propertiesMapping)
        {
            if (propertiesMapping == null)
            {
                return false;
            }
            else
            {
                Type t = this.GetType();
                IList<PropertyInfo> properties = t.GetProperties();

                // Split the CSV values
                string[] values = csvString.Split(new char[1] { ',' });

                // for each property set its value from the CSV
                foreach (PropertyInfo property in properties)
                {
                    if (propertiesMapping.Mapping.Keys.Contains(property.Name))
                    {
                        if (property.GetType() == typeof(DateTime))
                        {
                            if (propertiesMapping.Mapping[property.Name] >= 0 && propertiesMapping.Mapping[property.Name] < values.Length)
                            {
                                DateTime tmp;
                                DateTime.TryParse(values[propertiesMapping.Mapping[property.Name]], out tmp);
                                property.SetValue(this, tmp, null);
                            }
                        }
                        else if (property.GetType() == typeof(short))
                        {
                            if (propertiesMapping.Mapping[property.Name] >= 0 && propertiesMapping.Mapping[property.Name] < values.Length)
                            {
                                double tmp;
                                double.TryParse(values[propertiesMapping.Mapping[property.Name]], out tmp);
                                property.SetValue(this, Convert.ToInt16(tmp), null);
                            }
                        }
                        else if (property.GetType() == typeof(double))
                        {
                            if (propertiesMapping.Mapping[property.Name] >= 0 && propertiesMapping.Mapping[property.Name] < values.Length)
                            {
                                double tmp;
                                double.TryParse(values[propertiesMapping.Mapping[property.Name]], out tmp);
                                property.SetValue(this, tmp, null);
                            }
                        }
                        else if (property.GetType() == typeof(string))
                        {
                            if (propertiesMapping.Mapping[property.Name] >= 0 && propertiesMapping.Mapping[property.Name] < values.Length)
                            {
                                property.SetValue(this, values[propertiesMapping.Mapping[property.Name]], null);
                            }
                        }
                    }
                }

                return true;
            }
        }
    }
}