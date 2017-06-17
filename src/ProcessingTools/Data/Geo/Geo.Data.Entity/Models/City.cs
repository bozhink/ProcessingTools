namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    public class City : SystemInformation, ISynonymisable<CitySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IDataModel
    {
        private ICollection<PostCode> postCodes;
        private ICollection<CitySynonym> synonyms;

        public City()
        {
            this.postCodes = new HashSet<PostCode>();
            this.synonyms = new HashSet<CitySynonym>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfCityName)]
        [MaxLength(ValidationConstants.MaximalLengthOfCityName)]
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

        public virtual int? CountyId { get; set; }

        public virtual County County { get; set; }

        public virtual ICollection<PostCode> PostCodes
        {
            get
            {
                return this.postCodes;
            }

            set
            {
                this.postCodes = value;
            }
        }

        public virtual ICollection<CitySynonym> Synonyms
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
