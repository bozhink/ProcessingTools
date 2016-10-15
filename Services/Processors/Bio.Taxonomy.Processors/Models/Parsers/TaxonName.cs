namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Constants;

    public class TaxonName
    {
        private const long PositionDefaultValue = 0L;

        public TaxonName()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Position = PositionDefaultValue;
            this.Parts = new HashSet<TaxonNamePart>();
        }

        public TaxonName(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[XmlInternalSchemaConstants.IdAttributeName]?.InnerText ?? string.Empty;
            this.Type = node.Attributes[XmlInternalSchemaConstants.TypeAttributeName]?.InnerText ?? string.Empty;

            var positionAttributeValue = node.Attributes[XmlInternalSchemaConstants.PositionAttributeName]?.InnerText ?? string.Empty;
            long position = PositionDefaultValue;
            if (long.TryParse(positionAttributeValue, out position))
            {
                this.Position = position;
            }
            else
            {
                this.Position = PositionDefaultValue;
            }

            this.Parts = new HashSet<TaxonNamePart>();
            foreach (XmlNode taxonNamePart in node.SelectNodes(XmlInternalSchemaConstants.TaxonNamePartElementName))
            {
                this.Parts.Add(new TaxonNamePart(taxonNamePart));
            }
        }

        public string Id { get; set; }

        public long Position { get; set; }

        public string Type { get; set; }

        public ICollection<TaxonNamePart> Parts { get; set; }
    }
}
