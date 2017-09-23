namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Models.Contracts;

    public class County : SystemInformation, ISynonymisable<CountySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IDataModel
    {
        private ICollection<City> cities;
        private ICollection<CountySynonym> synonyms;

        public County()
        {
            this.cities = new HashSet<City>();
            this.synonyms = new HashSet<CountySynonym>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfStateName)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual int? StateId { get; set; }

        public virtual State State { get; set; }

        public virtual int? ProvinceId { get; set; }

        public virtual Province Province { get; set; }

        public virtual int? RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual int? DistrictId { get; set; }

        public virtual District District { get; set; }

        public virtual int? MunicipalityId { get; set; }

        public virtual Municipality Municipality { get; set; }

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

        public virtual ICollection<CountySynonym> Synonyms
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
