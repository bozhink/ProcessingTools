namespace ProcessingTools.Geo.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class Country
    {
        private ICollection<City> cities;
        private ICollection<State> states;

        public Country()
        {
            this.cities = new HashSet<City>();
            this.states = new HashSet<State>();
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
    }
}