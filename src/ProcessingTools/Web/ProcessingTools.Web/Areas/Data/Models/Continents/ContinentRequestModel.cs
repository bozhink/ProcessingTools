namespace ProcessingTools.Web.Areas.Data.Models.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public class ContinentRequestModel : IContinent
    {
        public ContinentRequestModel()
        {
            this.Countries = new HashSet<ICountry>();
            this.Synonyms = new HashSet<IContinentSynonym>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName)]
        public string AbbreviatedName { get; set; }

        public ICollection<ICountry> Countries { get; set; }

        public ICollection<IContinentSynonym> Synonyms { get; set; }
    }
}