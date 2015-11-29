namespace ProcessingTools.BaseLibrary.Taxonomy
{
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
}