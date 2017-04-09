namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Models;

    public class City : IDataModel
    {
        private ICollection<PostCode> postCodes;

        public City()
        {
            this.postCodes = new HashSet<PostCode>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfCityName)]
        public string Name { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual int? StateId { get; set; }

        public virtual State State { get; set; }

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
    }
}
