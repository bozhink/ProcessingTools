namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class Country
    {
        private ICollection<City> cities;
        private ICollection<State> states;
        private ICollection<Continent> continents;
        private ICollection<CountrySynonym> synonyms;

        public Country()
        {
            this.cities = new HashSet<City>();
            this.states = new HashSet<State>();
            this.continents = new HashSet<Continent>();
            this.synonyms = new HashSet<CountrySynonym>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfCallingCode)]
        public string CallingCode { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfLanguageCode)]
        public string LanguageCode { get; set; }

        public string Iso639xCode { get; set; }

        public virtual ICollection<City> Cities
        {
            get
            {
                return this.cities;
            }

            set
            {
                this.cities = value;
            }
        }

        public virtual ICollection<State> States
        {
            get
            {
                return this.states;
            }

            set
            {
                this.states = value;
            }
        }

        public virtual ICollection<Continent> Continents
        {
            get
            {
                return this.continents;
            }

            set
            {
                this.continents = value;
            }
        }

        public virtual ICollection<CountrySynonym> Synonyms
        {
            get
            {
                return this.synonyms;
            }

            set
            {
                this.synonyms = value;
            }
        }
    }
}
