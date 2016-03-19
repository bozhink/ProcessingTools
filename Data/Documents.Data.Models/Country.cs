namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Country
    {
        private ICollection<City> cities;
        private ICollection<Publisher> publishers;

        public Country()
        {
            this.cities = new HashSet<City>();
            this.publishers = new HashSet<Publisher>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

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

        public virtual ICollection<Publisher> Publishers
        {
            get
            {
                return this.publishers;
            }

            set
            {
                this.publishers = value;
            }
        }
    }
}