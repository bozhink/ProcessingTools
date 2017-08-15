namespace ProcessingTools.Processors.Models.Bio.Taxonomy.Parsers
{
    using System;
    using System.Linq.Expressions;
    using System.Xml;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Bio.Taxonomy.Parsers;

    internal class TaxonNamePart : MinimalTaxonNamePart, ITaxonNamePart
    {
        public TaxonNamePart(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.Id = node.Attributes[AttributeNames.Id]?.InnerText ?? string.Empty;

            this.Name = node.InnerText;

            var typeAttribute = node.Attributes[AttributeNames.Type];
            if (typeAttribute != null)
            {
                this.Rank = typeAttribute.InnerText.ToSpeciesPartType();
            }
            else
            {
                this.Rank = SpeciesPartType.Undefined;
            }

            this.FullName = node.Attributes[AttributeNames.FullName]?.InnerText;
        }

        public string Id { get; set; }

        public bool IsAbbreviated { get; private set; } = false;

        public bool IsResolved { get; private set; } = false;

        public bool IsModified { get; set; } = false;

        public override string Name
        {
            get
            {
                return base.Name;
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

                base.Name = (value ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(base.Name))
                {
                    this.Pattern = "# Never match pattern";
                }
                else
                {
                    if (this.IsAbbreviated)
                    {
                        this.Pattern = base.Name.TrimEnd('.');
                    }
                    else
                    {
                        this.Pattern = base.Name;
                    }
                }
            }
        }

        public override string FullName
        {
            get
            {
                return base.FullName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Contains("."))
                {
                    this.IsResolved = false;
                    if (this.IsAbbreviated)
                    {
                        base.FullName = string.Empty;
                    }
                    else
                    {
                        base.FullName = this.Name;
                    }
                }
                else
                {
                    this.IsResolved = true;
                    base.FullName = value.Trim();
                }
            }
        }

        public string Pattern { get; private set; }

        public Expression<Func<ITaxonNamePart, bool>> MatchExpression
        {
            get
            {
                if (this.IsAbbreviated && !this.IsResolved)
                {
                    return p => (p.Rank == this.Rank) &&
                                (!p.IsAbbreviated || p.IsResolved) &&
                                (p.FullName.IndexOf(this.Pattern) == 0);
                }
                else
                {
                    return p => (p.Rank == this.Rank) &&
                                (!p.IsAbbreviated || p.IsResolved) &&
                                (p.FullName == this.FullName);
                }
            }
        }

        public override int GetHashCode()
        {
            return (this.FullName + this.Rank + this.Name).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
