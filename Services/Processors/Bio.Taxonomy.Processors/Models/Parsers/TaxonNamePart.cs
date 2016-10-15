namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System;
    using System.Linq.Expressions;
    using System.Xml;

    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.Types;

    internal class TaxonNamePart : MinimalTaxonNamePart, ITaxonNamePart
    {
        private string name;

        public TaxonNamePart()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public TaxonNamePart(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[XmlInternalSchemaConstants.IdAttributeName]?.InnerText ?? string.Empty;
            this.Name = node.InnerText;

            var typeAttribute = node.Attributes[XmlInternalSchemaConstants.TypeAttributeName];
            if (typeAttribute != null)
            {
                this.Rank = typeAttribute.InnerText.ToSpeciesPartType();
            }
            else
            {
                this.Rank = SpeciesPartType.Undefined;
            }

            var fullNameAttribute = node.Attributes[XmlInternalSchemaConstants.FullNameAttributeName];
            if (fullNameAttribute == null)
            {
                if (this.IsAbbreviated)
                {
                    this.FullName = string.Empty;
                }
                else
                {
                    this.FullName = this.Name;
                }
            }
            else
            {
                this.FullName = fullNameAttribute.InnerText.Trim();
            }
        }

        public string Id { get; set; }

        public bool IsAbbreviated { get; private set; }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Contains("."))
                {
                    this.IsAbbreviated = true;
                }
                else
                {
                    this.IsAbbreviated = false;
                }

                this.name = value.Trim();

                if (this.IsAbbreviated)
                {
                    this.Pattern = this.name.TrimEnd('.');
                }
                else
                {
                    this.Pattern = this.name;
                }
            }
        }

        public string Pattern { get; private set; }

        public bool IsModified { get; set; } = false;

        public Expression<Func<ITaxonNamePart, bool>> MatchExpression
        {
            get
            {
                if (this.IsAbbreviated && !this.IsModified)
                {
                    return p => (p.Rank == this.Rank) &&
                                (!p.IsAbbreviated || p.IsModified) &&
                                (p.FullName.IndexOf(this.Pattern) == 0);
                }
                else
                {
                    return p => (p.Rank == this.Rank) &&
                                (!p.IsAbbreviated || p.IsModified) &&
                                (p.FullName == this.FullName);
                }
            }
        }

        public override string ToString()
        {
            return this.FullName + " " + this.Rank;
        }

        public override int GetHashCode()
        {
            return (this.FullName + this.Rank + this.Name).GetHashCode();
        }
    }
}
