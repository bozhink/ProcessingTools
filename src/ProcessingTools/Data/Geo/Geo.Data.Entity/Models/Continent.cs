namespace ProcessingTools.Geo.Data.Entity.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Data.Geo.Models;
    using ProcessingTools.Contracts.Models;

    public class Continent : SystemInformation, ISynonymisable<ContinentSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IDataModel
    {
        private ICollection<Country> countries;
        private ICollection<ContinentSynonym> synonyms;

        public Continent()
        {
            this.synonyms = new HashSet<ContinentSynonym>();
            this.countries = new HashSet<Country>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

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
