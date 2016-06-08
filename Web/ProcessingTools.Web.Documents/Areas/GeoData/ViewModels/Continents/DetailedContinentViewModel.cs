namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Continents
{
    using System.Collections.Generic;
    using ViewModels.Countries;

    public class DetailedContinentViewModel : ContinentViewModel
    {
        public DetailedContinentViewModel()
        {
            this.Synonyms = new HashSet<ContinentSynonymViewModel>();
            this.Countries = new HashSet<CountryViewModel>();
        }

        public ICollection<ContinentSynonymViewModel> Synonyms { get; set; }

        public ICollection<CountryViewModel> Countries { get; set; }
    }
}