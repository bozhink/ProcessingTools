namespace ProcessingTools.Bio.Taxonomy.Processors.Models.Parsers
{
    using System.Text.RegularExpressions;

    public class Species
    {
        private Regex genusNameMatchInXml = new Regex("(?<=type=\"genus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private Regex subgenusNameMatchInXml = new Regex("(?<=type=\"subgenus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private Regex speciesNameMatchInXml = new Regex("(?<=type=\"species\"[^>]*>)[a-z\\-\\.]+(?=</t)");
        private Regex subspeciesNameMatchInXml = new Regex("(?<=type=\"subspecies\"[^>]*>)[a-z\\-]+(?=</t)");

        private string genus;
        private string subgenus;
        private string species;
        private string subspecies;

        public Species(string parsedContent)
        {
            Match m = this.genusNameMatchInXml.Match(parsedContent);
            this.genus = m.Success ? m.Value : string.Empty;

            m = this.subgenusNameMatchInXml.Match(parsedContent);
            this.subgenus = m.Success ? m.Value : string.Empty;

            m = this.speciesNameMatchInXml.Match(parsedContent);
            this.species = m.Success ? m.Value : string.Empty;

            m = this.subspeciesNameMatchInXml.Match(parsedContent);
            this.subspecies = m.Success ? m.Value : string.Empty;
        }

        public string GenusName => this.genus;

        public string SubgenusName => this.subgenus;

        public string SpeciesName => this.species;

        public string SubspeciesName => this.subspecies;

        public string GenusPattern
        {
            get
            {
                return (this.genus.IndexOf('.') > -1) ? "\\b" + this.genus.Substring(0, this.genus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.genus + "\\b";
            }
        }

        public string SubgenusPattern
        {
            get
            {
                return (this.subgenus.IndexOf('.') > -1) ? "\\b" + this.subgenus.Substring(0, this.subgenus.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.subgenus + "\\b";
            }
        }

        public string SpeciesPattern
        {
            get
            {
                return (this.species.IndexOf('.') > -1) ? "\\b" + this.species.Substring(0, this.species.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.species + "\\b";
            }
        }

        public override string ToString()
        {
            string name = (this.genus.Length == 0) ? string.Empty : this.genus;
            name += (this.subgenus.Length == 0) ? string.Empty : " (" + this.subgenus + ")";
            name += (this.species.Length == 0) ? string.Empty : " " + this.species;
            name += (this.subspecies.Length == 0) ? string.Empty : " " + this.subspecies;
            return name;
        }
    }
}
