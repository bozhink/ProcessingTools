namespace ProcessingTools.BaseLibrary.Taxonomy
{
    using System.Text.RegularExpressions;

    public class SpeciesComparison
    {
        private bool equalGenera;
        private bool equalSubgenera;
        private bool equalSpecies;
        private bool equalSubspecies;

        public SpeciesComparison()
        {
            this.equalGenera = false;
            this.equalSubgenera = false;
            this.equalSpecies = false;
            this.equalSubspecies = false;
        }

        public SpeciesComparison(Species sp1, Species sp2)
        {
            this.equalGenera = string.Compare(sp1.GenusName, sp2.GenusName) == 0;
            this.equalSubgenera = string.Compare(sp1.SubgenusName, sp2.SubgenusName) == 0;
            this.equalSpecies = string.Compare(sp1.SpeciesName, sp2.SpeciesName) == 0;
            this.equalSubspecies = string.Compare(sp1.SubspeciesName, sp2.SubspeciesName) == 0;
        }

        public bool EqualGenera
        {
            get
            {
                return this.equalGenera;
            }
        }

        public bool EqualSubgenera
        {
            get
            {
                return this.equalSubgenera;
            }
        }

        public bool EqaulSpecies
        {
            get
            {
                return this.equalSpecies;
            }
        }

        public bool EqualSubspecies
        {
            get
            {
                return this.equalSubspecies;
            }
        }
    }

    public class Species
    {
        private static Regex genusNameMatchInXml = new Regex("(?<=type=\"genus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private static Regex subgenusNameMatchInXml = new Regex("(?<=type=\"subgenus\"[^>]*>)[A-Z][a-z\\.]+(?=</t)");
        private static Regex speciesNameMatchInXml = new Regex("(?<=type=\"species\"[^>]*>)[a-z\\-\\.]+(?=</t)");
        private static Regex subspeciesNameMatchInXml = new Regex("(?<=type=\"subspecies\"[^>]*>)[a-z\\-]+(?=</t)");

        private string genus;
        private string subgenus;
        private string species;
        private string subspecies;
        private bool nullGenus;
        private bool nullSubgenus;
        private bool nullSpecies;
        private bool nullSubspecies;
        private bool shortened;

        public Species(string parsedContent)
        {
            Match m = Species.genusNameMatchInXml.Match(parsedContent);
            this.genus = m.Success ? m.Value : string.Empty;
            m = Species.subgenusNameMatchInXml.Match(parsedContent);
            this.subgenus = m.Success ? m.Value : string.Empty;
            m = Species.speciesNameMatchInXml.Match(parsedContent);
            this.species = m.Success ? m.Value : string.Empty;
            m = Species.subspeciesNameMatchInXml.Match(parsedContent);
            this.subspecies = m.Success ? m.Value : string.Empty;
            this.nullGenus = this.CheckIfGenusIsNull();
            this.nullSubgenus = this.CheckIfSubgenusIsNull();
            this.nullSpecies = this.CheckIfSpeciesIsNull();
            this.nullSubspecies = this.CheckIfSubspeciesIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public Species(string genus, string subgenus, string species, string subspecies)
        {
            this.genus = genus;
            this.subgenus = subgenus;
            this.species = species;
            this.subspecies = subspecies;
            this.nullGenus = this.CheckIfGenusIsNull();
            this.nullSubgenus = this.CheckIfSubgenusIsNull();
            this.nullSpecies = this.CheckIfSpeciesIsNull();
            this.nullSubspecies = this.CheckIfSubspeciesIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public Species()
        {
            this.genus = string.Empty;
            this.subgenus = string.Empty;
            this.species = string.Empty;
            this.subspecies = string.Empty;
            this.nullGenus = true;
            this.nullSubgenus = true;
            this.nullSpecies = true;
            this.nullSubspecies = true;
            this.shortened = false;
        }

        public Species(Species sp)
        {
            this.genus = sp.GenusName;
            this.subgenus = sp.SubgenusName;
            this.species = sp.SpeciesName;
            this.subspecies = sp.SubspeciesName;
            this.nullGenus = this.CheckIfGenusIsNull();
            this.nullSubgenus = this.CheckIfSubgenusIsNull();
            this.nullSpecies = this.CheckIfSpeciesIsNull();
            this.nullSubspecies = this.CheckIfSubspeciesIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public string GenusName
        {
            get
            {
                return this.genus;
            }
        }

        public string SubgenusName
        {
            get
            {
                return this.subgenus;
            }
        }

        public string SpeciesName
        {
            get
            {
                return this.species;
            }
        }

        public string SubspeciesName
        {
            get
            {
                return this.subspecies;
            }
        }

        public bool IsGenusNull
        {
            get
            {
                return this.nullGenus;
            }
        }

        public bool IsSubgenusNull
        {
            get
            {
                return this.nullSubgenus;
            }
        }

        public bool IsSpeciesNull
        {
            get
            {
                return this.nullSpecies;
            }
        }

        public bool IsSubspeciesNull
        {
            get
            {
                return this.nullSubspecies;
            }
        }

        public bool IsShortened
        {
            get
            {
                return this.shortened;
            }
        }

        public string GenusTagged
        {
            get
            {
                return "<tn-part type=\"genus\">" + this.genus + "</tn-part>";
            }
        }

        public string SubgenusTagged
        {
            get
            {
                return "<tn-part part-type=\"subgenus\">" + this.subgenus + "</tn-part>";
            }
        }

        public string SpeciesTagged
        {
            get
            {
                return "<tn-part type=\"species\">" + this.species + "</tn-part>";
            }
        }

        public string SubspeciesTaged
        {
            get
            {
                return "<tn-part type=\"subspecies\">" + this.subspecies + "</tn-part>";
            }
        }

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

        public string SubspeciesPattern
        {
            get
            {
                return (this.subspecies.IndexOf('.') > -1) ? "\\b" + this.subspecies.Substring(0, this.subspecies.IndexOf('.')) + "[a-z\\-]+?" : "\\b" + this.subspecies + "\\b";
            }
        }

        public string GenusSkipPattern
        {
            get
            {
                return (this.genus.IndexOf('.') > -1) ? "\\b" + this.genus.Substring(0, this.genus.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
            }
        }

        public string SubgenusSkipPattern
        {
            get
            {
                return (this.subgenus.IndexOf('.') > -1) ? "\\b" + this.subgenus.Substring(0, this.subgenus.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
            }
        }

        public string SpeciesSkipPattern
        {
            get
            {
                return (this.species.IndexOf('.') > -1) ? "\\b" + this.species.Substring(0, this.species.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
            }
        }

        public string SubspeciesSkipPattern
        {
            get
            {
                return (this.subspecies.IndexOf('.') > -1) ? "\\b" + this.subspecies.Substring(0, this.subspecies.IndexOf('.')) + "[a-z\\-]+?" : "SKIP";
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

        public string SpeciesNameGenusSubgenusAsString
        {
            get
            {
                string name = this.nullGenus ? string.Empty : this.genus;
                name += this.nullSubgenus ? string.Empty : " (" + this.subgenus + ")";
                name += this.nullSpecies ? string.Empty : " [" + this.species + "]";
                name += this.nullSubspecies ? string.Empty : " [" + this.subspecies + "]";
                return name;
            }
        }

        public string AsString()
        {
            string result = string.Empty;
            if (!this.nullGenus)
            {
                result += this.genus;
            }

            if (!this.nullSubgenus)
            {
                if (result.Length != 0)
                {
                    result += " ";
                }

                result += "(" + this.subgenus + ")";
            }

            if (!this.nullSpecies)
            {
                if (result.Length != 0)
                {
                    result += " ";
                }

                result += this.species;
            }

            if (!this.nullSubgenus)
            {
                if (result.Length != 0)
                {
                    result += " ";
                }

                result += this.subspecies;
            }

            return result;
        }

        public void SetGenus(string genus)
        {
            this.genus = genus;
            this.nullGenus = this.CheckIfGenusIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSubgenus(string subgenus)
        {
            this.subgenus = subgenus;
            this.nullSubgenus = this.CheckIfSubgenusIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSpecies(string species)
        {
            this.species = species;
            this.nullSpecies = this.CheckIfSpeciesIsNull();
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSubspecies(string subspecies)
        {
            this.subspecies = subspecies;
            this.nullSubspecies = this.CheckIfSubspeciesIsNull();
        }

        public void SetGenus(Species sp)
        {
            this.genus = sp.GenusName;
            this.nullGenus = sp.IsGenusNull;
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSubgenus(Species sp)
        {
            this.subgenus = sp.SubgenusName;
            this.nullSubgenus = sp.IsSubgenusNull;
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSpecies(Species sp)
        {
            this.species = sp.SpeciesName;
            this.nullSpecies = sp.IsSpeciesNull;
            this.shortened = this.CheckIfTaxonIsShortened();
        }

        public void SetSubspecies(Species sp)
        {
            this.subspecies = sp.SubspeciesName;
            this.nullSubspecies = sp.IsSubspeciesNull;
        }

        private bool CheckIfGenusIsNull()
        {
            return this.genus.Length == 0;
        }

        private bool CheckIfSubgenusIsNull()
        {
            return this.subgenus.Length == 0;
        }

        private bool CheckIfSpeciesIsNull()
        {
            return this.species.Length == 0;
        }

        private bool CheckIfSubspeciesIsNull()
        {
            return this.subspecies.Length == 0;
        }

        private bool CheckIfTaxonIsShortened()
        {
            string sp = this.genus + this.subgenus + this.species + this.subspecies;
            return sp.IndexOf('.') > -1;
        }
    }
}
