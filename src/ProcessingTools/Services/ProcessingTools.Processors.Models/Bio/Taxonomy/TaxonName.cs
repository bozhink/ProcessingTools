// <copyright file="TaxonName.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Xml;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Models.Contracts.Processors.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Taxon name.
    /// </summary>
    public class TaxonName : ITaxonName
    {
        private const long PositionDefaultValue = 0L;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonName"/> class.
        /// </summary>
        /// <param name="node"><see cref="XmlNode"/> for initialization.</param>
        public TaxonName(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[AttributeNames.Id]?.InnerText ?? string.Empty;

            var typeAttribute = node.Attributes[AttributeNames.Type];
            if (typeAttribute != null)
            {
                this.Type = typeAttribute.InnerText.MapTaxonTypeStringToTaxonType();
            }
            else
            {
                this.Type = TaxonType.Undefined;
            }

            var positionAttribute = node.Attributes[AttributeNames.Position];
            long position = PositionDefaultValue;
            if (positionAttribute != null && long.TryParse(positionAttribute.InnerText, out position))
            {
                this.Position = position;
            }
            else
            {
                this.Position = PositionDefaultValue;
            }

            var parts = node.SelectNodes(ElementNames.TaxonNamePart)
                .Cast<XmlNode>()
                .Select(n => new TaxonNamePart(n))
                .ToArray<ITaxonNamePart>();

            this.Parts = parts.AsQueryable();
        }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public long Position { get; set; }

        /// <inheritdoc/>
        public TaxonType Type { get; set; }

        /// <inheritdoc/>
        public IQueryable<ITaxonNamePart> Parts { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} | {3}", this.Id, this.Type, this.Position, string.Join(" / ", this.Parts));
        }
    }
}
