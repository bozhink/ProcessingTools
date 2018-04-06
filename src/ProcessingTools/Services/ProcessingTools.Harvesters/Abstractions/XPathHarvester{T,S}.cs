// <copyright file="XPathHarvester{T,S}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Attributes;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Harvesters.Contracts;

    /// <summary>
    /// Generic XPath harvester.
    /// </summary>
    /// <typeparam name="T">Type of resultant model.</typeparam>
    /// <typeparam name="S">Type of internal model.</typeparam>
    public abstract class XPathHarvester<T, S> : IXmlHarvester<T>
        where S : T, new()
    {
        private readonly IDictionary<PropertyInfo, PropertyData> propertyDictionary;
        private readonly Regex matchWhitespace = new Regex(@"\s+");

        /// <summary>
        /// Initializes a new instance of the <see cref="XPathHarvester{T, S}"/> class.
        /// </summary>
        protected XPathHarvester()
        {
            this.propertyDictionary = new Dictionary<PropertyInfo, PropertyData>();

            var properties = typeof(S).GetProperties().ToArray();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(XPathAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    this.propertyDictionary[property] = new PropertyData
                    {
                        Converter = TypeDescriptor.GetConverter(property.PropertyType),
                        Attributes = attributes
                    };
                }
            }
        }

        /// <inheritdoc/>
        public Task<T> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run<T>(() =>
            {
                var result = new S();

                foreach (var item in this.propertyDictionary)
                {
                    foreach (XPathAttribute attribute in item.Value.Attributes)
                    {
                        var node = context.SelectSingleNode(attribute.XPath);
                        if (node != null)
                        {
                            string value = node.InnerText;

                            if (node.Name == ElementNames.PubDate || node.Name == ElementNames.Date)
                            {
                                var dateNodes = node.SelectNodes($"{ElementNames.Day}|{ElementNames.Month}|{ElementNames.Year}")
                                    .Cast<XmlNode>()
                                    .ToList();

                                if (dateNodes.Any())
                                {
                                    int year = GetInteger(dateNodes, ElementNames.Year);
                                    int month = GetInteger(dateNodes, ElementNames.Month);
                                    int day = GetInteger(dateNodes, ElementNames.Day);

                                    value = $"{year}-{month}-{day}";
                                }
                            }

                            value = this.matchWhitespace.Replace(value, " ").Trim();
                            if (item.Key.PropertyType == typeof(string))
                            {
                                item.Key.SetValue(result, value);
                            }
                            else
                            {
                                if (item.Value.Converter != null)
                                {
                                    item.Key.SetValue(result, item.Value.Converter.ConvertFromString(value));
                                }
                            }

                            break;
                        }
                    }
                }

                return result;
            });
        }

        private static int GetInteger(IEnumerable<XmlNode> nodes, string nodeName)
        {
            int.TryParse(nodes.FirstOrDefault(n => n.Name == nodeName)?.InnerText?.Trim(), out int result);
            return result;
        }

        /// <summary>
        /// Private class to represent needed <see cref="PropertyInfo" /> data.
        /// </summary>
        private class PropertyData
        {
            /// <summary>
            /// Gets or sets the type converter.
            /// </summary>
            internal TypeConverter Converter { get; set; }

            /// <summary>
            /// Gets or sets the attributes.
            /// </summary>
            internal IEnumerable Attributes { get; set; }
        }
    }
}
