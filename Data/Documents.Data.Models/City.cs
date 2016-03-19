namespace ProcessingTools.Documents.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class City
    {
        private ICollection<Country> countries;
        private ICollection<Address> addresses;

        public City()
        {
            this.countries = new HashSet<Country>();
            this.addresses = new HashSet<Address>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfCityName)]
        public string Name { get; set; }

        public virtual ICollection<Country> Countries
        {
            get
            {
                return this.countries;
            }

            set
            {
                this.countries = value;
            }
        }

        public virtual ICollection<Address> Addresses
        {
            get
            {
                return this.addresses;
            }

            set
            {
                this.addresses = value;
            }
        }
    }
}