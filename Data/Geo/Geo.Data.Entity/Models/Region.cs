﻿namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Data.Geo.Models;
    using ProcessingTools.Contracts.Models;

    public class Region : SystemInformation, ISynonymisable<RegionSynonym>, IDataModel
    {
        private ICollection<District> districts;
        private ICollection<Municipality> municipalities;
        private ICollection<County> counties;
        private ICollection<City> cities;
        private ICollection<RegionSynonym> synonyms;

        public Region()
        {
            this.districts = new HashSet<District>();
            this.municipalities = new HashSet<Municipality>();
            this.counties = new HashSet<County>();
            this.cities = new HashSet<City>();
            this.synonyms = new HashSet<RegionSynonym>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedStateName)]
        public string AbbreviatedName { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual int? StateId { get; set; }

        public virtual State State { get; set; }

        public virtual int? ProvinceId { get; set; }

        public virtual Province Province { get; set; }

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

        public virtual ICollection<RegionSynonym> Synonyms
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