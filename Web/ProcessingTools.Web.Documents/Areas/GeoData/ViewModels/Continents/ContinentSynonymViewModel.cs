namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Continents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ContinentSynonymViewModel : ContinentViewModel
    {
        public ContinentSynonymViewModel()
        {
            this.Id = (Guid.NewGuid().ToString() + DateTime.UtcNow.ToLongTimeString()).GetHashCode();
        }

        [Display(Name = "Continent Id")]
        public int ContinentId { get; set; }
    }
}
