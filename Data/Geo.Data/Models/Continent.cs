namespace ProcessingTools.Geo.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class Continent
    {
        private ICollection<ContinentSynonym> synonyms;
        private ICollection<Country> countries;

        public Continent()
        {
            this.synonyms = new HashSet<ContinentSynonym>();
            this.countries = new HashSet<Country>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        public virtual ICollection<ContinentSynonym> Synonyms
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
    }
}
