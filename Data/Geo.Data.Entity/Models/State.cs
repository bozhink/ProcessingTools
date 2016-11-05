namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class State
    {
        private ICollection<City> cities;
        private ICollection<State> states;

        public State()
        {
            this.cities = new HashSet<City>();
            this.states = new HashSet<State>();
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
