namespace ProcessingTools.BaseLibrary.Taxonomy
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
        private bool nullGenus;
        private bool nullSubgenus;
        private bool nullSpecies;
        private bool nullSubspecies;

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

            this.nullGenus = this.CheckIfGenusIsNull();
            this.nullSubgenus = this.CheckIfSubgenusIsNull();
            this.nullSpecies = this.CheckIfSpeciesIsNull();
            this.nullSubspecies = this.CheckIfSubspeciesIsNull();
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

        public string SpeciesNameAsString
        {
            get
            {
                string name = this.nullGenus ? string.Empty : this.genus;
                name += this.nullSubgenus ? string.Empty : " (" + this.subgenus + ")";
                name += this.nullSpecies ? string.Empty : " " + this.species;
                name += this.nullSubspecies ? string.Empty : " " + this.subspecies;
                return name;
            }
        }

        private bool CheckIfGenusIsNull() => this.genus.Length == 0;

        private bool CheckIfSubgenusIsNull() => this.subgenus.Length == 0;

        private bool CheckIfSpeciesIsNull() => this.species.Length == 0;

        private bool CheckIfSubspeciesIsNull() => this.subspecies.Length == 0;
    }
}
