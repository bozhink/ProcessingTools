// <copyright file="XPathHarvester{T,S}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Attributes;
    using ProcessingTools.Harvesters.Contracts;

    /// <summary>
    /// XPath harvester.
    /// </summary>
    /// <typeparam name="T">Type of resultant model.</typeparam>
    /// <typeparam name="S">Type of internal model.</typeparam>
    public abstract class XPathHarvester<T, S> : IXmlHarvester<T>
        where S : T, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XPathHarvester{T, S}"/> class.
        /// </summary>
        protected XPathHarvester()
        {
        }

        /// <inheritdoc/>
        public Task<T> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var result = new S();

            var properties = typeof(S).GetProperties().Where(p => p.PropertyType == typeof(string)).ToArray();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(XPathAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    foreach (XPathAttribute attribute in attributes)
                    {
                        var node = context.SelectSingleNode(attribute.XPath);
                        if (node != null)
                        {
                            property.SetValue(result, node.InnerXml);
                            break;
                        }
                    }
                }
            }

            return Task.FromResult<T>(result);
        }
    }
}
