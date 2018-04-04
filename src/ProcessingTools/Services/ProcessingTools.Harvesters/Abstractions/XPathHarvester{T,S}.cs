// <copyright file="XPathHarvester{T,S}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Attributes;
    using ProcessingTools.Harvesters.Contracts;

    /// <summary>
    /// Generic XPath harvester.
    /// </summary>
    /// <typeparam name="T">Type of resultant model.</typeparam>
    /// <typeparam name="S">Type of internal model.</typeparam>
    public abstract class XPathHarvester<T, S> : IXmlHarvester<T>
        where S : T, new()
    {
        private readonly IDictionary<PropertyInfo, IEnumerable> dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="XPathHarvester{T, S}"/> class.
        /// </summary>
        protected XPathHarvester()
        {
            this.dictionary = new Dictionary<PropertyInfo, IEnumerable>();

            var properties = typeof(S).GetProperties().Where(p => p.PropertyType == typeof(string)).ToArray();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(XPathAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    this.dictionary[property] = attributes;
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

                foreach (var item in this.dictionary)
                {
                    foreach (XPathAttribute attribute in item.Value)
                    {
                        var node = context.SelectSingleNode(attribute.XPath);
                        if (node != null)
                        {
                            string value = node.InnerText;

                            if (node.HasChildNodes)
                            {
                                var dateNodes = node.SelectNodes("day|month|year").Cast<XmlNode>();
                                if (dateNodes.Any())
                                {
                                    int year = GetInteger(dateNodes, "year");
                                    int month = GetInteger(dateNodes, "month");
                                    int day = GetInteger(dateNodes, "day");

                                    value = $"{year}-{month}-{day}";
                                }
                            }

                            item.Key.SetValue(result, Regex.Replace(value, @"\s+", " ").Trim());
                            break;
                        }
                    }
                }

                return result;
            });
        }

        private static int GetInteger(IEnumerable<XmlNode> nodes, string nodeName)
        {
            int.TryParse(nodes.FirstOrDefault(n => n.Name == nodeName)?.InnerText?.Trim(), out int year);
            return year;
        }
    }
}
