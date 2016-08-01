namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ViewModels.Countries;

    public class DetailedContinentViewModel : ContinentViewModel
    {
        public DetailedContinentViewModel()
        {
            this.Synonyms = new HashSet<ContinentSynonymViewModel>();
            this.Countries = new HashSet<CountryViewModel>();
        }

        [Display(Name = "Synonyms")]
        public ICollection<ContinentSynonymViewModel> Synonyms { get; set; }

        [Display(Name = "Countries")]
        public ICollection<CountryViewModel> Countries { get; set; }
    }
}