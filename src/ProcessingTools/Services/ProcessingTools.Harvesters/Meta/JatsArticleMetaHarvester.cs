// <copyright file="JatsArticleMetaHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Attributes;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Meta;

    /// <summary>
    /// JATS Article meta harvester.
    /// </summary>
    public class JatsArticleMetaHarvester : IJatsArticleMetaHarvester
    {
        /// <inheritdoc/>
        public Task<IArticleMetaModel> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var result = new JatsArticleMetaModel();

            var type = typeof(JatsArticleMetaModel);

            var properties = type.GetProperties().Where(p => p.PropertyType == typeof(string)).ToArray();

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

            return Task.FromResult<IArticleMetaModel>(result);
        }
    }
}
