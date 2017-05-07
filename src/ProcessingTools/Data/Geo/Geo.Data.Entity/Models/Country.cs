namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Data.Geo.Models;
    using ProcessingTools.Contracts.Models;

    public class Country : SystemInformation, ISynonymisable<CountrySynonym>, INameableIntegerIdentifiable, IDataModel
    {
        private ICollection<Continent> continents;
        private ICollection<State> states;
        private ICollection<Province> provinces;
        private ICollection<Region> regions;
        private ICollection<District> districts;
        private ICollection<Municipality> municipalities;
        private ICollection<County> counties;
        private ICollection<City> cities;
        private ICollection<CountrySynonym> synonyms;

        public Country()
        {
            this.continents = new HashSet<Continent>();
            this.states = new HashSet<State>();
            this.provinces = new HashSet<Province>();
            this.regions = new HashSet<Region>();
            this.districts = new HashSet<District>();
            this.municipalities = new HashSet<Municipality>();
            this.counties = new HashSet<County>();
            this.cities = new HashSet<City>();
            this.synonyms = new HashSet<CountrySynonym>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfCountryName)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfCallingCode)]
        public string CallingCode { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfLanguageCode)]
        public string LanguageCode { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfIso639xCode)]
        public string Iso639xCode { get; set; }

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

        public virtual ICollection<Province> Provinces
        {
            get
            {
                return this.provinces;
            }

            set
            {
                this.provinces = value;
            }
        }

        public virtual ICollection<Region> Regions
        {
            get
            {
                return this.regions;
            }

            set
            {
                this.regions = value;
            }
        }

        public virtual ICollection<District> Districts
        {
            get
            {
                return this.districts;
            }

            set
            {
                this.districts = value;
            }
        }

        public virtual ICollection<Municipality> Municipalities
        {
            get
            {
                return this.municipalities;
            }

            set
            {
                this.municipalities = value;
            }
        }

        public virtual ICollection<County> Counties
        {
            get
            {
                return this.counties;
            }

            set
            {
                this.counties = value;
            }
        }

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
