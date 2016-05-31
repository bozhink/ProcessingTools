namespace ProcessingTools.BaseLibrary.Taxonomy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using ProcessingTools.Bio.Taxonomy.Constants;

    public class TaxonName
    {
        public TaxonName()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Parts = new HashSet<TaxonNamePart>();
        }

        public TaxonName(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[XmlInternalSchemaConstants.IdAttributeName]?.InnerText ?? string.Empty;
            this.Type = node.Attributes[XmlInternalSchemaConstants.TaxonNameTypeAttributeName]?.InnerText ?? string.Empty;

            this.Parts = new HashSet<TaxonNamePart>();
            foreach (XmlNode taxonNamePart in node.SelectNodes(XmlInternalSchemaConstants.TaxonNamePartElementName))
            {
                this.Parts.Add(new TaxonNamePart(taxonNamePart));
            }
        }

        public string Id { get; set; }

        public string Type { get; set; }

        public ICollection<TaxonNamePart> Parts { get; set; }
    }
}
