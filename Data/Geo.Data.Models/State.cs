namespace ProcessingTools.Geo.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Constants;

    public class State
    {
        private ICollection<City> cities;

        public State()
        {
            this.cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfStateName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedStateName)]
        public string AbbreviatedName { get; set; }

        public StateType Type { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

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
    }
}
