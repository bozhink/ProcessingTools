namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;

    public class TaxonName : ITaxonName
    {
        private const long PositionDefaultValue = 0L;

        public TaxonName()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Position = PositionDefaultValue;
            this.Type = TaxonType.Undefined;
            this.Parts = new HashSet<ITaxonNamePart>();
        }

        public TaxonName(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[XmlInternalSchemaConstants.IdAttributeName]?.InnerText ?? string.Empty;

            var typeAttribute = node.Attributes[XmlInternalSchemaConstants.TypeAttributeName];
            if (typeAttribute != null)
            {
                this.Type = typeAttribute.InnerText.MapTaxonTypeStringToTaxonType();
            }
            else
            {
                this.Type = TaxonType.Undefined;
            }

            var positionAttribute = node.Attributes[XmlInternalSchemaConstants.PositionAttributeName];
            long position = PositionDefaultValue;
            if (positionAttribute != null && long.TryParse(positionAttribute.InnerText, out position))
            {
                this.Position = position;
            }
            else
            {
                this.Position = PositionDefaultValue;
            }

            this.Parts = new HashSet<ITaxonNamePart>();
            foreach (XmlNode taxonNamePart in node.SelectNodes(XmlInternalSchemaConstants.TaxonNamePartElementName))
            {
                this.Parts.Add(new TaxonNamePart(taxonNamePart));
            }
        }

        public string Id { get; set; }

        public long Position { get; set; }

        public TaxonType Type { get; set; }

        public ICollection<ITaxonNamePart> Parts { get; set; }
    }
}
